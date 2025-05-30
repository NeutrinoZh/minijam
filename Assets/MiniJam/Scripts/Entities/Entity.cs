using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class Entity : MonoBehaviour
    {
        public Action OnDeath;
        public Action<float> OnHit;

        [SerializeField] private EntityCharacteristics _characteristics;

        public const float BaseAttackDuration = 10;

        public float MoveSpeed { get; set; }
        public float RangeOfAttack { get; set; }
        public float AttackSpeed { get; set; }
        public float Attack { get; set; }
        public float Health { get; set; }
        public float RangeOfPickUp { get; set; }
        public bool CanMove { get; set; }
        public bool IsAlive { get; set; }
        public float MaxHealth => _characteristics.MaxHealth;

        private void Awake()
        {
            CanMove = true;
            IsAlive = true;
            MoveSpeed = _characteristics.MoveSpeed;
            RangeOfAttack = _characteristics.RangeOfAttack;
            AttackSpeed = _characteristics.AttackSpeed;
            Attack = _characteristics.Attack;
            Health = _characteristics.MaxHealth;
            RangeOfPickUp = _characteristics.RangeOfPickUp;
        }
    }
}