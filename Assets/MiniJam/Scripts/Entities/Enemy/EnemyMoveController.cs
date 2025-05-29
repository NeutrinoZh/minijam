using UnityEngine;
using Zenject;

namespace Game
{
    public class EnemyMoveController : MonoBehaviour
    {
        private Player _player;
        private Entity _entity;
        private Rigidbody _rigidbody;
        
        [Inject]
        public void Construct(Player player)
        {
            _player = player;
            _entity = GetComponent<Entity>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!_entity.CanMove || !_player) 
                return;
            
            var direction = (_player.transform.position - transform.position).normalized;
            _rigidbody.velocity = direction * _entity.MoveSpeed;
        }
    }
}