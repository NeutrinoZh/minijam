using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class ItemSlots : MonoBehaviour
    {
        [SerializeField] private List<ItemSlot> _itemSlots;
        private PlayerInventory _playerInventory;
        
        private int _freeSlotIndex;

        [Inject]
        public void Construct(PlayerInventory playerInventory)
        {
            _playerInventory = playerInventory;
        }

        private void Start()
        {
            _playerInventory.OnAddItem += OnNewItemAdded;
        }

        private void OnDestroy()
        {
            _playerInventory.OnAddItem -= OnNewItemAdded;
        }
        
        private void OnNewItemAdded(Item item)
        {
            if (_freeSlotIndex >= _itemSlots.Count)
                return;
            
            _itemSlots[_freeSlotIndex++].SetData(item);
        }
    }
}