using System;
using System.Collections.Generic;
using System.Linq;
using Game.InventorySystem;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Game
{
    public class PlayerInventory
    {
        public event Action<Item> OnAddItem; 
        public event Action<Item> OnLevelUpItem;
        
        private readonly Dictionary<string, float> _cacheOfBuffs = new();
        private readonly Dictionary<Item, int> _items = new();
        private readonly ItemsRegister _itemsRegister;
        
        [Inject]
        public PlayerInventory(ItemsRegister itemsRegister)
        {
            _itemsRegister = itemsRegister;
        }

        public bool IsFull()
        {
            return _items.Count == 3;
        }

        public bool HasItem(Type typeOfItem)
        {
            if (!typeOfItem.DerivesFrom(typeof(Item)))
                return false;
            
            var item = _itemsRegister.GetItemByType(typeOfItem);
            return _items.ContainsKey(item);
        }
        
        public void AddOrLevelUpItem(Type typeOfItem)
        {
            if (!typeOfItem.DerivesFrom(typeof(Item)))
                return;
            
            var item = _itemsRegister.GetItemByType(typeOfItem);

            if (!_items.TryAdd(item, 1))
            {
                _items[item] += 1;
                OnLevelUpItem?.Invoke(item);
            }
            else
                OnAddItem?.Invoke(item);
            
            _cacheOfBuffs.Clear();
        }

        public int GetLevelOfItem(Item item)
        {
            return _items.GetValueOrDefault(item, 0);
        }
        
        public float GetSumOfBuff(string key)
        {
            if (_cacheOfBuffs.TryGetValue(key, out var cache))
                return cache;
            
            var sum = (
                from item in _items 
                let properties = Utils.GetAllListProperties(item.Key).ToList()
                let propertyByKey = properties.Find(x => x.Name == key) 
                where propertyByKey != null
                let listOfValues = (List<float>)propertyByKey.GetValue(item.Key) 
                select item.Value >= listOfValues.Count ? listOfValues.Last() : listOfValues[item.Value])
                .Sum() + 1;
            
            _cacheOfBuffs.Add(key, sum);
            return sum;
        }
    }
}