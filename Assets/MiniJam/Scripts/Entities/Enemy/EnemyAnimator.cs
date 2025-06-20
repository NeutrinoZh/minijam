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
        private static readonly int k_mainTex = Shader.PropertyToID("_MainTex");

        private Animator _animator;
        private Entity _entity;
        
        private EnemyMeleeFight _enemyMeleeFight;
        private EnemyRangeFight _enemyRangeFight;

        private AnimationClip _attackClip;
        private AnimationClip _deathClip;
        
        private void Awake()
        {
            _entity = GetComponent<Entity>();
            _animator = GetComponentInChildren<Animator>();
            _enemyMeleeFight = GetComponent<EnemyMeleeFight>();
            _enemyRangeFight = GetComponent<EnemyRangeFight>();
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
            
            if (_enemyMeleeFight) _enemyMeleeFight.OnAttack += AttackHandle;
            if (_enemyRangeFight) _enemyRangeFight.OnAttack += AttackHandle;
        }

        private void OnDestroy()
        {
            _entity.OnDeath -= DeathHandle;
            
            if (_enemyMeleeFight) _enemyMeleeFight.OnAttack -= AttackHandle;
            if (_enemyRangeFight) _enemyRangeFight.OnAttack -= AttackHandle;
        }

        private void DeathHandle()
        {
            const float duration = 2;
            
            var renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var skinned in renderers)
            {
                var instance = new Material(_deathMaterial);
                var texture = skinned.material.mainTexture;
                
                skinned.material = instance;
                skinned.material.mainTexture = texture;
                skinned.material.SetTexture(k_mainTex, texture);
                
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