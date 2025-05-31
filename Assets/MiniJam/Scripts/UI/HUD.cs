using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private ValueBar _healthBar;
        [SerializeField] private ValueBar _experienceBar;
        [SerializeField] private TextMeshProUGUI _levelLabel;
        
        private Player _player;
        private Entity _playerAsEntity;
        private string _levelPattern;
        
        [Inject]
        public void Construct(Player player)
        {
            _player = player;
            _playerAsEntity = _player.GetComponent<Entity>();
        }

        private void Awake()
        {
            _levelPattern = _levelLabel.text;
            UpdateLevelLabel();
        }

        private void Start()
        {
            _player.OnLevelUp += UpdateLevelLabel;
        }

        private void OnDestroy()
        {
            _player.OnLevelUp -= UpdateLevelLabel;
        }

        private void UpdateLevelLabel()
        {
            _levelLabel.text = _levelPattern.Replace("{}", (_player.Level + 1).ToString());
        }

        private void Update()
        {
            if (!_player)
                return;
                
            _healthBar.Value = _playerAsEntity.Health / _playerAsEntity.MaxHealth;
            _experienceBar.Value = _player.Experience / _player.RequiredExperienceForLevelUp;
        }
    }
}