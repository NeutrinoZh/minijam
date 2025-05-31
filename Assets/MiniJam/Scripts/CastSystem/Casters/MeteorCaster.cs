using System;
using System.Linq;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game
{
    [SpellCaster(SpellType = typeof(MeteorSpell))]
    public class MeteorCaster : Caster
    {
        public float Damage => 
            (Level >= _spell.Damage.Count ? _spell.Damage.Last() : _spell.Damage[Level]) * PlayerInventory.GetSumOfBuff(nameof(Damage));
        public float Projectiles =>
            (Level >= _spell.Projectiles.Count ? _spell.Projectiles.Last() : _spell.Projectiles[Level]) * PlayerInventory.GetSumOfBuff(nameof(Projectiles));
        public float Delay => _spell.Delay;
        
        private readonly MeteorSpell _spell;
        private readonly DiContainer _container;
        
        [Inject]
        public MeteorCaster(DiContainer container, PlayerInventory inventory, MeteorSpell spell):
            base(spell, inventory)
        {
            _container = container;
            _spell = spell;
        }

        protected override void CastInternal(Transform caster)
        {
            for (var i = 0; i < Projectiles; i++)
            {
                var randomOffset = Random.insideUnitCircle * Random.Range(_spell.Range.x, _spell.Range.y);
                var position = caster.position + new Vector3(randomOffset.x, -0.4f, randomOffset.y);

                var sight = Object.Instantiate(_spell.SightPrefab);
                sight.transform.position = position;

                var clone = _container.InstantiatePrefabForComponent<MeteorProjectile>(_spell.ProjectilePrefab);
                clone.Construct(this);
                clone.transform.position = position + Vector3.up * 15;

                Action onCollision = null;
                onCollision = () =>
                {
                    clone.OnCollisionFloor -= onCollision;
                    SpawnExplosion(position);
                };

                clone.OnCollisionFloor += onCollision;
            }
        }

        private void SpawnExplosion(Vector3 position)
        {
            var explosion = Object.Instantiate(_spell.ExplosionPrefab);
            explosion.transform.position = position;   
        }
    }
}