using System.Collections;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Chest : MonoBehaviour
    {
        [SerializeField] private float _animationDuration;
        
        private ItemSelectionScreen _itemSelectionScreen;
        private Animator _animator;
        
        [Inject]
        public void Construct(ItemSelectionScreen itemSelectionScreen)
        {
            _itemSelectionScreen = itemSelectionScreen;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.TryGetComponent(out Player player)) 
                return;

            StartCoroutine(StartDelayRoutine());
        }

        private IEnumerator StartDelayRoutine()
        {
            yield return new WaitForSeconds(_animationDuration);
            _itemSelectionScreen.Show();
            Destroy(gameObject);
        }
    }
}