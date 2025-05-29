using UnityEngine;
using Zenject;

namespace Game
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private Controls _controls;
        
        [Inject]
        public void Construct(Controls controls)
        {
            _controls = controls;
        }

        private void Update()
        {
            var direction = _controls.Actions.Player.Move.ReadValue<Vector2>();
            var velocity = Time.deltaTime * _speed * new Vector3(direction.x, 0, direction.y);
            
            transform.Translate( velocity);
        }
    }
}