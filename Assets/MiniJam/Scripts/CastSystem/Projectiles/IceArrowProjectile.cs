using UnityEngine;

namespace Game
{
    public class IceArrowProjectile : MonoBehaviour
    {
        private Vector3 _direction;
        private Rigidbody _rigidbody;
        private IceArrowCaster _caster;
        
        public void Construct(IceArrowCaster caster, Vector3 direction)
        {
            _caster = caster;
            _direction = direction;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            transform.right = _direction;
            _rigidbody.velocity = _direction * _caster.Speed;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (!collision.transform.TryGetComponent(out EntityDamagable damagable))
                return;
            
            damagable.Damage(_caster.Damage);
        }
    }
}