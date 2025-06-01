using UnityEngine;
using Zenject;

namespace Game
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Bounds _bounds;
        
        public Vector2 Direction => _controls.Actions.Player.Move.ReadValue<Vector2>();
        
        private Controls _controls;
        private Entity _playerAsEntity;
        
        [Inject]
        public void Construct(Controls controls)
        {
            _controls = controls;
        }

        private void Awake()
        {
            _playerAsEntity = GetComponent<Entity>();
        }
        
        private void Update()
        {
            if (!_playerAsEntity.CanMove || !_playerAsEntity.IsAlive)
                return;
            
            var direction = Direction;
            if (direction == Vector2.zero)
                return;
            
            var moveDirection = new Vector3(direction.x, 0, direction.y);
            var velocity = Time.deltaTime * _speed * moveDirection;
            
            var targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            
            var futurePosition = transform.position + velocity;
            if (_bounds.Contains(futurePosition))
                transform.position = futurePosition;
        }
    }
}