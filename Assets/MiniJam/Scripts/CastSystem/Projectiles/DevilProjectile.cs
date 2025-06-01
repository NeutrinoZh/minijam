using UnityEngine;

namespace Game
{
    public class DevilProjectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private Vector3 _direction;
        private Rigidbody _rigidbody;
        private float _damage;
        
        public void Construct(float damage, Vector3 direction)
        {
            _damage = damage;
            _direction = direction;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            transform.right = _direction;
            _rigidbody.velocity = _direction * _speed;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (!collision.transform.TryGetComponent(out EntityDamagable damagable))
                return;
            
            damagable.Damage(_damage);
        }
    }
}