using UnityEngine;
using Zenject;

namespace Game
{
    public class Friend : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _gainDamageByLevel;

        private Player _player;

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
        }
        
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out EntityDamagable damagable))
                damagable.Damage(_damage + (_gainDamageByLevel * _player.Level));
        }
    }
}