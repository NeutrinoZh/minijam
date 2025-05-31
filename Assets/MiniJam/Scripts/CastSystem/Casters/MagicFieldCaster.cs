using System.Linq;
using UnityEngine;
using Zenject;

namespace Game
{
    [SpellCaster(SpellType = typeof(MagicFieldSpell))]
    public class MagicFieldCaster : Caster
    {
        public float Duration => Level >= _spell.Duration.Count ? _spell.Duration.Last() : _spell.Duration[Level];
        public float Radius =>
            (Level >= _spell.Radius.Count ? _spell.Radius.Last() : _spell.Radius[Level]) * PlayerInventory.GetSumOfBuff(nameof(Radius));
        public float Damage => 
            (Level >= _spell.Damage.Count ? _spell.Damage.Last() : _spell.Damage[Level]) * PlayerInventory.GetSumOfBuff(nameof(Damage));

        private readonly MagicFieldSpell _spell;
        private readonly DiContainer _container;
        
        [Inject]
        public MagicFieldCaster(DiContainer container, PlayerInventory inventory, MagicFieldSpell spell):
            base(spell, inventory)
        {
            _container = container;
            _spell = spell;
        }
        
        protected override void CastInternal(Transform caster)
        {
            var clone = _container.InstantiatePrefabForComponent<MagicFieldProjectile>(_spell.ProjectilePrefab, caster);
            clone.Construct(this);
            clone.transform.position = caster.position;
        }
    }
}