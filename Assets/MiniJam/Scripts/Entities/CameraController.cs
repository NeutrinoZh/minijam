using UnityEngine;
using Zenject;

namespace Game
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Bounds _bounds;
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private float _cameraSpeed;
        
        private Transform _player;
        
        [Inject]
        public void Construct(Player player)
        {
            _player = player.transform;
        }

        private void Update()
        {
            if (!_player)
                return;
                
            var position = Vector3.Lerp(transform.position, _player.position + _cameraOffset, _cameraSpeed * Time.deltaTime);
            
            position = new Vector3(
                Mathf.Clamp(position.x, _bounds.min.x, _bounds.max.x),
                Mathf.Clamp(position.y, _bounds.min.y,  _bounds.max.y),
                Mathf.Clamp(position.z, _bounds.min.z,  _bounds.max.z));
            
            transform.position = position;
        }
    }
}