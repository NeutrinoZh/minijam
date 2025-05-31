using System.Linq;
using UnityEngine;
using Zenject;

namespace Game
{
    [SpellCaster(SpellType = typeof(IceArrowSpell))]
    public class IceArrowCaster : Caster
    {
        public float Damage => 
            (Level >= _spell.Damage.Count ? _spell.Damage.Last() : _spell.Damage[Level]) * PlayerInventory.GetSumOfBuff(nameof(Damage));
        public float Speed => Level >= _spell.Speed.Count ? _spell.Speed.Last() : _spell.Speed[Level];
        
        private readonly IceArrowSpell _spell;
        private readonly ListOfObject<Enemy> _enemies;
        private readonly DiContainer _container;
        
        [Inject]
        public IceArrowCaster(DiContainer container, PlayerInventory inventory, IceArrowSpell spell, ListOfObject<Enemy> enemies):
            base(spell, inventory)
        {
            _container = container;
            _spell = spell;
            _enemies = enemies;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void CastInternal(Transform caster)
        {
            var nearestEnemy = _enemies.GetNearestTo(caster.position);
            var direction = (nearestEnemy.transform.position -  caster.position).normalized;
            
            var clone = _container.InstantiatePrefabForComponent<IceArrowProjectile>(_spell.ProjectilePrefab);
            clone.Construct(this, direction);
            clone.transform.position = caster.position;
        }
    }
}