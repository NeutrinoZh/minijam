using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class EnemyAnimator : MonoBehaviour
    {
        private static readonly int k_deathId = Animator.StringToHash("Base Layer.Death");
        private static readonly int k_attackId = Animator.StringToHash("Base Layer.Attack");

        private Animator _animator;
        private Entity _entity;
        private EnemyMeleeFight _enemyMeleeFight;

        private AnimationClip _attackClip;
        
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
            _animator.Play(k_deathId);
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
    }
}