using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game
{
    public class FriendMoveController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _maxSqrDistanceFromPlayer;
        
        private ListOfObject<Enemy> _enemies;
        private Player _player;

        private Rigidbody _rigidbody;
        
        private Entity _target;
        private Entity _playerAsEntity;
        
        [Inject]
        public void Construct(ListOfObject<Enemy> enemies, Player player)
        {
            _enemies = enemies;
            _player = player;
            _rigidbody = GetComponent<Rigidbody>();
            _playerAsEntity = _player.GetComponent<Entity>();
        }

        private void Start()
        {
            _target = _playerAsEntity;
        }
        
        private void FixedUpdate()
        {
            if (!_target || !_target.IsAlive || _target == _playerAsEntity)
            {
                var enemy = _enemies.GetNearestTo(_player.transform.position);
                var offset = enemy ? enemy.transform.position - transform.position : Vector3.zero;
                
                if (!enemy || offset.sqrMagnitude > _maxSqrDistanceFromPlayer)
                    _target = _playerAsEntity;
                else
                    _target = enemy.GetComponent<Entity>();
            }

            var direction = (_target.transform.position - transform.position).normalized;
            
            _rigidbody.AddForce(direction * _speed);
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
            
            transform.forward = direction;
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        }
    }
}