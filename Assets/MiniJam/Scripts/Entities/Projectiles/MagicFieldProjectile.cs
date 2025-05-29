using System;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.VFX;
using Zenject;

namespace Game
{
    public class MagicFieldProjectile : MonoBehaviour
    {
        private static readonly int k_lifetimeId = Shader.PropertyToID("Lifetime");
        private static readonly int k_sizeId = Shader.PropertyToID("Size");
        
        private VisualEffect _visualEffect;
        private SphereCollider _collider;
        private MagicFieldCaster _caster;
        
        private List<EntityDamagable> _entities = new();
        
        public void Construct(MagicFieldCaster caster)
        {
            _caster = caster;
        }
        
        private void Awake()
        {
            _collider = GetComponentInChildren<SphereCollider>();
            _visualEffect = GetComponentInChildren<VisualEffect>();
        }
        
        private void Start()
        {
            if (TryGetComponent(out TempObject tempObject))
                tempObject.TimeOfLife = _caster.Duration;

            _visualEffect.SetFloat(k_lifetimeId, _caster.Duration);
            _visualEffect.SetFloat(k_sizeId, _caster.Radius * 2f);
            _collider.radius = 0;

            Tween.Custom(
                0, 
                _caster.Radius,
                _caster.Duration / 2,
                value => _collider.radius = value,
                cycles: 2,
                cycleMode: CycleMode.Yoyo
                );
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.transform.TryGetComponent(out EntityDamagable damagable)) 
                return;
            
            if (_entities.Contains(damagable))
                return;
                    
            _entities.Add(damagable);
            damagable.Damage(_caster.Damage);
        }
    }
}