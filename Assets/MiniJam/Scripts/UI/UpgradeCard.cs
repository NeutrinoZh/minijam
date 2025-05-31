using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class UpgradeCard : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _titleLabel;
        [SerializeField] private TextMeshProUGUI _levelLabel;
        [SerializeField] private TextMeshProUGUI _descriptionLabel;
        
        [SerializeField] private TextMeshProUGUI _namesLabel;
        [SerializeField] private TextMeshProUGUI _valuesLabel;

        public event Action OnSelect;
        
        private string k_levelPattern;
        private PlayerCaster _playerCaster;
        private Spell _spell;

        [Inject]
        public void Construct(Player player)
        {
            _playerCaster = player.GetComponent<PlayerCaster>();
        }
        
        public void SetSpell(Spell spell)
        {
            _spell = spell;
            
            var spellCaster = _playerCaster.GetCasterOfSpell(spell.GetType());
            var spellLevel = (spellCaster?.Level ?? -1) + 2;
            
            _iconImage.sprite = spell.Icon;
            _titleLabel.text = spell.Title;
            _levelLabel.text = k_levelPattern.Replace("{}", spellLevel.ToString());
            _descriptionLabel.text = spell.Description;

            var properties = Utils.GetAllListProperties(spell);
            var namesText = ""; 
            var valuesText = "<mspace=0.54em>";

            foreach (var property in properties)
            {
                var list = (List<float>)property.GetValue(spell);
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
            var spellType = _spell.GetType();
            var spellCaster = _playerCaster.GetCasterOfSpell(spellType);
            
            if (spellCaster != null)
                spellCaster.Level += 1;
            else 
                _playerCaster.AddSpell(spellType);
            
            OnSelect?.Invoke();
        }
    }
}