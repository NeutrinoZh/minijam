using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game
{
    public class EnemyRangeFight : MonoBehaviour
    {
        public event Action<float> OnAttack;

        [SerializeField] private DevilProjectile _projectile;
        
        private DiContainer _container;
        private Entity _entity;
        private Player _player;
        
        private bool _attacking = false;
        
        [Inject]
        public void Construct(DiContainer container, Player player)
        {
            _player = player;
            _container = container;
            _entity = GetComponent<Entity>();
        }
        
        private void Update()
        {
            if (_attacking || !_player || !_entity.IsAlive)
                return;
            
            var offset = _player.transform.position - transform.position;
            if (!(offset.magnitude < _entity.RangeOfAttack))
                return;
            
            _attacking = true;
            _entity.CanMove = false;

            var duration = Entity.BaseAttackDuration / _entity.AttackSpeed;
            OnAttack?.Invoke(duration);
            
            StartCoroutine(Attack(duration));
        }
        
        private IEnumerator Attack(float duration)
        {
            yield return new WaitForSeconds(duration);
            
            _attacking = false;
            _entity.CanMove = true;

            var direction = (_player.transform.position - transform.position).normalized;
            
            var clone = _container.InstantiatePrefabForComponent<DevilProjectile>(_projectile);
            clone.transform.position = transform.position;
            clone.Construct(_entity.Attack, direction);
        }
    }
}