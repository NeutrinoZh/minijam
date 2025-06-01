using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class EnemyMiniBoss : MonoBehaviour
    {
        [SerializeField] private Transform _chest;
        
        private Entity _entity;
        private ItemSelectionScreen _itemSelectionScreen;
        private DiContainer _container;
        
        [Inject]
        public void Construct(DiContainer container, ItemSelectionScreen itemSelectionScreen)
        {
            _container = container;
            _itemSelectionScreen = itemSelectionScreen;
            _entity = GetComponent<Entity>();
        }
        
        private void Start()
        {
            _entity.OnDeath += HandleDeath;
        }

        private void OnDestroy()
        {
            _entity.OnDeath -= HandleDeath;
        }

        private void HandleDeath()
        {
            var clone = _container.InstantiatePrefab(_chest);
            clone.transform.position = new Vector3(
                transform.position.x,
                -7.5f,
                transform.position.z);
        }
    }
}