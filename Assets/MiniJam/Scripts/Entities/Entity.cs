using System;
using UnityEngine;

namespace Game
{
    public class Entity : MonoBehaviour
    {
        public Action OnDeath;
        public Action<float> OnHit;
        
        [SerializeField] private EntityCharacteristicsSo _characteristicsSo;

        public const float BaseAttackDuration = 10;
        
        public float MoveSpeed { get; set; }
        public float RangeOfAttack { get; set; }
        public float AttackSpeed { get; set; }
        public float Attack { get; set; }
        public float Health { get; set; }
        public bool CanMove { get; set; }

        private void Awake()
        {
            CanMove = true;
            MoveSpeed = _characteristicsSo.MoveSpeed;
            RangeOfAttack = _characteristicsSo.RangeOfAttack;
            AttackSpeed = _characteristicsSo.AttackSpeed;
            Attack = _characteristicsSo.Attack;
            Health = _characteristicsSo.MaxHealth;
        }
    }
}