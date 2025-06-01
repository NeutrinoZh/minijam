using UnityEngine;
using Zenject;

namespace Game
{
    public class EnemyMoveController : MonoBehaviour
    {
        private Entity _player;
        private Entity _entity;
        private Rigidbody _rigidbody;
        
        [Inject]
        public void Construct(Player player)
        {
            _player = player.GetComponent<Entity>();
            _entity = GetComponent<Entity>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!_entity.IsAlive || !_entity.CanMove) 
                return;
            
            var direction = (_player.transform.position - transform.position).normalized;
            if (!_player.IsAlive)
                direction *= -1;
                
            _rigidbody.velocity = direction * _entity.MoveSpeed;
            transform.forward = direction;
        }
    }
}