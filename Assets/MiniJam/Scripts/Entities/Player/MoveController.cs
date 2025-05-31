using UnityEngine;
using Zenject;

namespace Game
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        
        private Controls _controls;
        
        [Inject]
        public void Construct(Controls controls)
        {
            _controls = controls;
        }

        private void Update()
        {
            var direction = _controls.Actions.Player.Move.ReadValue<Vector2>();
            if (direction == Vector2.zero)
                return;
            
            var moveDirection = new Vector3(direction.x, 0, direction.y);
            var velocity = Time.deltaTime * _speed * moveDirection;
            
            var targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime); 
            transform.position += velocity;
        }
    }
}