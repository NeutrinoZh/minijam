using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class SpellSlots : MonoBehaviour
    {
        [SerializeField] private List<SpellSlot> _spellSlots;
        private PlayerCaster _playerCaster;

        private int _freeSlotIndex = 0;

        [Inject]
        public void Construct(Player player)
        {
            _playerCaster = player.GetComponent<PlayerCaster>();
        }
        
        private void Start()
        {
            _playerCaster.OnAddNewSpell += OnNewSpellAdded;
        }

        private void OnDestroy()
        {
            _playerCaster.OnAddNewSpell -= OnNewSpellAdded;
        }
        
        private void OnNewSpellAdded(Caster caster)
        {
            if (_freeSlotIndex >= _spellSlots.Count)
                return;
            
            _spellSlots[_freeSlotIndex++].SetData(caster);
        }
    }
}