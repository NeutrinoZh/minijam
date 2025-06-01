using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game
{
    [DefaultExecutionOrder(-1)]
    public class ItemSelectionScreen : MonoBehaviour
    {
        [SerializeField] private List<ItemCard> _itemsCards;
        
        private List<Item> _items;
        
        private PlayerInventory _inventory;

        [Inject]
        public void Construct(PlayerInventory inventory)
        {
            _inventory = inventory;
        }

        private void Awake()
        {
            _items = Resources.LoadAll<Item>("Items").ToList();
        }

        private void Start()
        {
            foreach (var card in _itemsCards)
                card.OnSelect += Hide;
            Hide();
        }

        private void OnDestroy()
        {
            foreach (var card in _itemsCards)
                card.OnSelect -= Hide;
        }
        
        public void Show()
        {
            Time.timeScale = 0.01f;
            gameObject.SetActive(true);
            
            var possibleItems = _items.ToList();
            foreach (
                var item in from item in _items
                where item != null && _inventory.GetLevelOfItem(item) >= item.MaxLevel
                where _inventory.IsFull() && !_inventory.HasItem(item.GetType())
                select item
            )
            {
                possibleItems.Remove(item);
            }
            
            var numberOfVariants = possibleItems.Count >= 3 ? 3 : possibleItems.Count;
            for (var i = 0; i < numberOfVariants; ++i)
            {
                var randomItem = possibleItems[Random.Range(0, possibleItems.Count)];
                possibleItems.Remove(randomItem);
                
                SetCard(i, randomItem);
            }   
        }
        
        public void Hide()
        {
            Time.timeScale = 1;
            
            foreach (var card in _itemsCards) 
                card.gameObject.SetActive(false);   
            gameObject.SetActive(false);
        }
        
        private void SetCard(int indexOfSelection, Item spell)
        {
            _itemsCards[indexOfSelection].gameObject.SetActive(true);
            _itemsCards[indexOfSelection].SetItem(spell);
        }
    }
}