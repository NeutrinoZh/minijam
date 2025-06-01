using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Game
{
    public class PlayerCaster : MonoBehaviour
    {
        public event Action<Caster> OnAddNewSpell;
        
        private CastersRegister _castersRegister;
        private readonly Dictionary<Type, Caster> _casters = new();
        private Entity _playerAsEntity;
        
        [Inject]
        public void Construct(CastersRegister casterRegister)
        {
            _castersRegister = casterRegister;
        }

        private void Awake()
        {
            _playerAsEntity = GetComponent<Entity>();
        }
        
        public void AddSpell(Type spellType)
        {
            if (!spellType.DerivesFrom(typeof(Spell)))
                return;
            
            var caster = _castersRegister.CreateNewCasterForSpell(spellType);
            _casters.Add(spellType, caster);
            
            StartCoroutine(StartCasting(caster));
            
            OnAddNewSpell?.Invoke(caster);
        }
        
        public bool IsFull()
        {
            return _casters.Count == 4;
        }

        public bool HasSpell(Type typeOfSpell)
        {
            return typeOfSpell.DerivesFrom(typeof(Item)) && _casters.ContainsKey(typeOfSpell);
        }

        public Caster GetCasterOfSpell(Type type)
        {
            return _casters.GetValueOrDefault(type, null);
        }
        
        private IEnumerator StartCasting(Caster caster)
        {
            yield return new WaitForSeconds(1);
            
            while (_playerAsEntity.IsAlive)
            {
                caster.Cast(transform);
                yield return new WaitForSeconds(caster.Cooldown);
            }
        }
    }
}