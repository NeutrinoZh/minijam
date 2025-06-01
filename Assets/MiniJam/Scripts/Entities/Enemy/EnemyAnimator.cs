using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PrimeTween;
using UnityEngine;

namespace Game
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Material _deathMaterial;
        
        private static readonly int k_deathId = Animator.StringToHash("Base Layer.Death");
        private static readonly int k_attackId = Animator.StringToHash("Base Layer.Attack");
        private static readonly int k_opacity = Shader.PropertyToID("_Opacity");

        private Animator _animator;
        private Entity _entity;
        private EnemyMeleeFight _enemyMeleeFight;

        private AnimationClip _attackClip;
        private AnimationClip _deathClip;
        
        private void Awake()
        {
            _entity = GetComponent<Entity>();
            _animator = GetComponentInChildren<Animator>();
            _enemyMeleeFight = GetComponent<EnemyMeleeFight>();
        }

        private void Start()
        {
            _attackClip = _animator
                .runtimeAnimatorController
                .animationClips
                .First(c => c.name == "Attack");
            
            _deathClip = _animator
                .runtimeAnimatorController
                .animationClips
                .First(c => c.name == "Death");
            
            _entity.OnDeath += DeathHandle;
            _enemyMeleeFight.OnAttack += AttackHandle;
        }

        private void OnDestroy()
        {
            _entity.OnDeath -= DeathHandle;
            _enemyMeleeFight.OnAttack -= AttackHandle;
        }

        private void DeathHandle()
        {
            const float duration = 2;
            
            var renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var skinned in renderers)
            {
                var instance = new Material(_deathMaterial) {
                    mainTexture = skinned.material.mainTexture
                };
                
                skinned.material = instance;
                
                Tween.Custom(
                    1f, 0f, duration - 0.5f,
                    value => instance.SetFloat(k_opacity, value),
                    startDelay: 0.5f
                );
            }
            
            _animator.speed = _deathClip.length / duration;
            _animator.Play(k_deathId);
            
            StartCoroutine(DeathRoutine(duration));
        }

        private void AttackHandle(float duration)
        { 
            _animator.speed = _attackClip.length / duration;
            _animator.Play(k_attackId);

            StartCoroutine(AttackRoutine(duration));
        }

        private IEnumerator AttackRoutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            _animator.speed = 1;
        }

        private IEnumerator DeathRoutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            Destroy(gameObject);
        }
    }
}