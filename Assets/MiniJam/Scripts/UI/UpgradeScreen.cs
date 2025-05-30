using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game
{
    public class UpgradeScreen : MonoBehaviour
    {
        [SerializeField] private List<UpgradeCard> _upgradeCards;
        
        private List<Spell> _spells;
        
        private PlayerCaster _playerCaster;
        private Player _player;
        
        [Inject]
        public void Construct(Player player)
        {
            _player = player;
            _playerCaster = player.GetComponent<PlayerCaster>();
        }
        
        private void Awake()
        {
            _spells = Resources.LoadAll<Spell>("Spells").ToList();
        }

        private void Start()
        {
            _player.OnLevelUp += Show;
            foreach (var card in _upgradeCards)
                card.OnSelect += Hide;
            
            Show();
        }

        private void OnDestroy()
        {
            _player.OnLevelUp -= Show;
            foreach (var card in _upgradeCards)
                card.OnSelect -= Hide;
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            
            for (var i = 0; i < 3; ++i)
            {
                var randomSpell = _spells[Random.Range(0, _spells.Count)];
                SetUpgradeSpell(i, randomSpell);
            }   
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        private void SetUpgradeSpell(int indexOfSelection, Spell spell)
        {
            _upgradeCards[indexOfSelection].gameObject.SetActive(true);
            _upgradeCards[indexOfSelection].SetSpell(spell);
        }
    }
}