using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class MeteorProjectile : MonoBehaviour
    {
        public event Action OnCollisionFloor;
        private Rigidbody _rigidbody;
        private int _floorLayer;
        private MeteorCaster _meteorCaster;
        private bool _startFalling;

        public void Construct(MeteorCaster caster)
        {
            _meteorCaster = caster;
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _floorLayer = LayerMask.NameToLayer("Terrain");
        }

        private void Start()
        {
            StartCoroutine(DelayRoutine());
        }
        
        private void Update()
        {
            if (!_startFalling)
                return;
            
            _rigidbody.velocity = Vector3.down * 10f;
        }

        private IEnumerator DelayRoutine()
        {
            yield return new WaitForSeconds(_meteorCaster.Delay);
            _startFalling = true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _floorLayer)
                OnCollisionFloor?.Invoke();
            else if (other.TryGetComponent(out EntityDamagable entityDamagable)) 
                entityDamagable.Damage(_meteorCaster.Damage);
        }
    }
}