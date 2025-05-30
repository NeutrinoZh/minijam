using UnityEngine;
using Zenject;

namespace Game
{
    public class Experience : MonoBehaviour
    {
        private const float k_speedOfItems = 10f;
        
        private Player _player;
        private Entity _playerAsEntity;
        private Rigidbody _rigidbody;
        
        [Inject]
        public void Construct(Player player)
        {
            if (!player)
                return;
            
            _player = player;
            _playerAsEntity = player.GetComponent<Entity>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (!_player)
                return;

            var offset = _player.transform.position - transform.position;
            var sqrDistance = offset.sqrMagnitude;

            if (sqrDistance > _playerAsEntity.RangeOfPickUp)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }

            _rigidbody.velocity = offset.normalized * k_speedOfItems;
        }
    }
}