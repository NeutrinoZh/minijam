using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Game
{
    public class FireBallProjectile : MonoBehaviour
    {
        private Vector3 _direction;
        private Rigidbody _rigidbody;
        private FireBallCaster _caster;
        
        public void Construct(FireBallCaster caster, Vector3 direction)
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
            _rigidbody.velocity = _direction * _caster.Speed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.transform.TryGetComponent(out EntityDamagable damagable))
                return;
            
            damagable.Damage(_caster.Damage);
            
            enabled = false;
            GetComponentInChildren<SphereCollider>().enabled = false;
            GetComponent<TempObject>().TimeOfLife = 2f;
            GetComponentInChildren<VisualEffect>().Stop();
            _rigidbody.velocity = Vector3.zero;
        }
    }
}