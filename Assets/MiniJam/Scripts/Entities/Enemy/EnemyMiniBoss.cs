using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class EnemyMiniBoss : MonoBehaviour
    {
        private Entity _entity;
        private ItemSelectionScreen _itemSelectionScreen;

        [Inject]
        public void Construct(ItemSelectionScreen itemSelectionScreen)
        {
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
            _itemSelectionScreen.Show();
        }
    }
}