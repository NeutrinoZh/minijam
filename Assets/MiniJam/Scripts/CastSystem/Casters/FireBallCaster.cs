using System.Linq;
using UnityEngine;
using Zenject;

namespace Game
{
    [SpellCaster(SpellType = typeof(FireBallSpell))]
    public class FireBallCaster : Caster
    {
        public float Damage => 
            (Level >= _spell.Damage.Count ? _spell.Damage.Last() : _spell.Damage[Level]) * PlayerInventory.GetSumOfBuff(nameof(Damage));
        public float Speed => Level >= _spell.Speed.Count ? _spell.Speed.Last() : _spell.Speed[Level];
        
        private readonly FireBallSpell _spell;
        private readonly ListOfObject<Enemy> _enemies;
        private readonly DiContainer _container;
        
        [Inject]
        public FireBallCaster(DiContainer container, PlayerInventory inventory, FireBallSpell spell, ListOfObject<Enemy> enemies): 
            base(spell, inventory)
        {
            _container = container;
            _spell = spell;
            _enemies = enemies;
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        protected override void CastInternal(Transform caster)
        {
            const float angleOffset = 25f;
            
            var nearestEnemy = _enemies.GetNearestTo(caster.position);
            var directionToEnemy = (nearestEnemy.transform.position - caster.position).normalized;
            
            for (var i = -1; i <= 1; ++i)
            { 
                var angle = angleOffset * i;
                var direction = Quaternion.AngleAxis(angle, Vector3.up) * directionToEnemy;
                
                var clone = _container.InstantiatePrefabForComponent<FireBallProjectile>(_spell.ProjectilePrefab);
                clone.Construct(this, direction);
                clone.transform.position = caster.position;
            }
        }
    }
}