using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Game
{
    public class CastersRegister
    {
        private readonly DiContainer _container;
        
        private readonly Dictionary<Type, Type> _casters = new(); 
        private readonly Dictionary<Type, Spell> _spells = new();
        
        public CastersRegister(DiContainer container)
        {
            _container = container;
            LoadCasters();    
        }
        
        private Spell LoadSpell(Type type)
        {
            if (!type.IsSubclassOf(typeof(Spell)))
                return null;
            
            if (_spells.TryGetValue(type, out var loadedSpell))
                return loadedSpell; 
        
            var spell = Resources.Load<Spell>( $"Spells/{type.Name}");
            _spells.Add(type, spell);
            return spell;
        }
        
        private void LoadCasters()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t =>
                    t.GetCustomAttribute<SpellCasterAttribute>() != null &&
                    typeof(Caster).IsAssignableFrom(t)
                );

            foreach (var casterType in types)
            {
                var spellType = casterType.GetCustomAttribute<SpellCasterAttribute>().SpellType;
                _casters.Add(spellType, casterType);
            }
        }
        
        public Caster CreateNewCasterForSpell(Type type)
        {
            if (!type.DerivesFrom(typeof(Spell)))
                return null;
                
            if (!_casters.TryGetValue(type, out var caster))
                return null;
            
            _container.Bind(type).FromInstance(LoadSpell(type)).WhenInjectedInto(caster);
            return (Caster)_container.Instantiate(caster);
        }
    }
}