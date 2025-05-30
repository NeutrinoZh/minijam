using System.Linq;
using UnityEngine;
using Zenject;

namespace Game
{
    [SpellCaster(SpellType = typeof(FireBallSpell))]
    public class FireBallCaster : Caster
    {
        public float Damage => Level >= _spell.Damage.Count ? _spell.Damage.Last() : _spell.Damage[Level];
        public float Speed => Level >= _spell.Speed.Count ? _spell.Speed.Last() : _spell.Speed[Level];
        
        private readonly FireBallSpell _spell;
        private ListOfObject<Enemy> _enemies;
        private DiContainer _container;
        
        [Inject]
        public FireBallCaster(DiContainer container, FireBallSpell spell, ListOfObject<Enemy> enemies) : base(spell)
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
            
            var clone = _container.InstantiatePrefabForComponent<FireBallProjectile>(_spell.ProjectilePrefab);
            clone.Construct(this, direction);
            clone.transform.position = caster.position;
        }
    }
}