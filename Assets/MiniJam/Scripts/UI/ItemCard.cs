using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class ItemCard : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _titleLabel;
        [SerializeField] private TextMeshProUGUI _levelLabel;
        [SerializeField] private TextMeshProUGUI _descriptionLabel;
        
        [SerializeField] private TextMeshProUGUI _namesLabel;
        [SerializeField] private TextMeshProUGUI _valuesLabel;
        
        public event Action OnSelect;
        
        private string k_levelPattern;
        
        private Item _item;
        private PlayerInventory _inventory;
        
        [Inject]
        public void Construct(PlayerInventory inventory)
        {
            _inventory = inventory;
        }

        public void SetItem(Item item)
        {
            _item = item;
            
            _iconImage.sprite = item.Icon;
            _titleLabel.text = item.Title;
            _levelLabel.text = k_levelPattern.Replace("{}", (_inventory.GetLevelOfItem(item) + 1).ToString());
            _descriptionLabel.text = item.Description;
            
            var properties = Utils.GetAllListProperties(item);
            var namesText = ""; 
            var valuesText = "<mspace=0.54em>";

            foreach (var property in properties)
            {
                var list = (List<float>)property.GetValue(item);
                var values = string.Join("/", list.Select(v => v.ToString("0.#")));

                namesText += $"{property.Name}:\n";
                valuesText += $"{values}\n";
            }

            _namesLabel.text = namesText;
            _valuesLabel.text = valuesText;
        }
        
        private void Awake()
        {
            k_levelPattern = _levelLabel.text;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            _inventory.AddOrLevelUpItem(_item.GetType());
            OnSelect?.Invoke();
        }
    }
}