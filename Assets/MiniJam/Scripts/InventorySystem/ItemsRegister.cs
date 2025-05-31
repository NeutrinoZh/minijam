using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.InventorySystem
{
    public class ItemsRegister
    {
        private const string k_itemsPath = "Items";
        private readonly Dictionary<Type, Item> _items;
        
        public ItemsRegister()
        {
            var items = Resources.LoadAll<Item>(k_itemsPath);
            _items = items.ToDictionary(item => item.GetType(), item => item);
        }

        public Item GetItemByType(Type type)
        {
            return _items.GetValueOrDefault(type, null);
        }
    }
}