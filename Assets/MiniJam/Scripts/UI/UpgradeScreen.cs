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
            Time.timeScale = 0.01f;
            gameObject.SetActive(true);

            var possibleSpells = _spells.ToList();

            foreach (
                var spell in from spell in _spells
                let caster = _playerCaster.GetCasterOfSpell(spell.GetType())
                where caster != null && caster.Level >= spell.MaxLevel
                select spell
            )
            {
                possibleSpells.Remove(spell);
            }

            var numberOfVariants = possibleSpells.Count >= 3 ? 3 : possibleSpells.Count;
            for (var i = 0; i < numberOfVariants; ++i)
            {
                var randomSpell = possibleSpells[Random.Range(0, possibleSpells.Count)];
                possibleSpells.Remove(randomSpell);
                
                SetUpgradeSpell(i, randomSpell);
            }   
        }

        public void Hide()
        {
            Time.timeScale = 1;
            
            foreach (var card in _upgradeCards) 
                card.gameObject.SetActive(false);   
            gameObject.SetActive(false);
        }
        
        private void SetUpgradeSpell(int indexOfSelection, Spell spell)
        {
            _upgradeCards[indexOfSelection].gameObject.SetActive(true);
            _upgradeCards[indexOfSelection].SetSpell(spell);
        }
    }
}