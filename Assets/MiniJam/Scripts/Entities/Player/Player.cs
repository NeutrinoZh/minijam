using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _baseRequiredExperienceForLevelUp;
        [SerializeField] private float _multiplierFactorOfRequiredExperienceByLevel;
        
        public event Action OnLevelUp;
        
        public int Level { get; private set; }
        public float Experience { get; private set; }
        public float RequiredExperienceForLevelUp { get; private set; }

        private PoolOfObject<Experience> _experiencePool;
        private Entity _playerAsEntity;
        private ItemSelectionScreen _itemSelectionScreen;
        
        [Inject]
        public void Construct(PoolOfObject<Experience> experiencePool, ItemSelectionScreen itemSelectionScreen)
        {
            _experiencePool = experiencePool;
            _itemSelectionScreen = itemSelectionScreen;
            _playerAsEntity = GetComponent<Entity>();
        }
        
        private void Awake()
        {
            RequiredExperienceForLevelUp = _baseRequiredExperienceForLevelUp;
        }

        private void Start()
        {
            _playerAsEntity.OnDeath += OnDeath;
        }

        private void OnDestroy()
        {
            _playerAsEntity.OnDeath -= OnDeath;
        }

        private void Update()
        {
            if (Keyboard.current.xKey.wasPressedThisFrame)
                _itemSelectionScreen.Show();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.transform.TryGetComponent(out Experience experience))
                return;
            
            _experiencePool.Destroy(experience);
            
            Experience += 1;
            if (Experience < RequiredExperienceForLevelUp) 
                return;
            
            Level += 1;
            Experience = 0;
            RequiredExperienceForLevelUp *= _multiplierFactorOfRequiredExperienceByLevel;
            
            OnLevelUp?.Invoke();
        }

        private void OnDeath()
        {
            gameObject.SetActive(false);
        }
    }
}