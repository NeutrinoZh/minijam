using UnityEngine;

namespace Game
{
    public class ExplosionProjectile : MonoBehaviour
    {
        private MeteorCaster _caster;

        public void Construct(MeteorCaster caster)
        {
            _caster = _caster;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out EntityDamagable damagable))
                damagable.Damage(_caster.Damage);
        }
    }
}