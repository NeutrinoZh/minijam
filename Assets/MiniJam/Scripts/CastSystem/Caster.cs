using System;
using System.Linq;
using UnityEngine;

namespace Game
{
    public abstract class Caster
    {
        protected Caster(Spell spell)
        {
            Spell = spell;
        }
        
        public event Action OnCast;
        public Spell Spell { get; private set; }
        public int Level { get; set; }
        
        public float Cooldown => Level >= Spell.Cooldown.Count ? Spell.Cooldown.Last() : Spell.Cooldown[Level];
        
        protected abstract void CastInternal(Transform caster);

        public void Cast(Transform caster)
        {
            CastInternal(caster);
            OnCast?.Invoke();
        }
    }
}