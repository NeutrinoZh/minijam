using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private ValueBar _healthBar;
        [SerializeField] private ValueBar _experienceBar;

        private Player _player;
        private Entity _playerAsEntity;
        
        [Inject]
        public void Construct(Player player)
        {
            _player = player;
            _playerAsEntity = _player.GetComponent<Entity>();
        }
        
        private void Update()
        {
            if (!_player)
                return;
                
            _healthBar.Value = _playerAsEntity.Health / _playerAsEntity.MaxHealth;
        }
    }
}