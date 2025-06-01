using System.Globalization;
using UnityEngine;
using Zenject;

namespace Game
{
    public class EntityDamagable : MonoBehaviour
    {
        [SerializeField] private DamageNumber _damageNumberPrefab;

        private Entity _entity;

        [Inject]
        public void Construct()
        {
            _entity = GetComponent<Entity>();
        }

        private void Start()
        {
            _entity.OnHit += ShowDamageNumber;
        }

        private void OnDestroy()
        {
            _entity.OnHit -= ShowDamageNumber;
        }

        public void Damage(float amount)
        {
            if (!_entity.IsAlive)
                return;

            _entity.Health -= amount;

            _entity.OnHit?.Invoke(amount);
            if (_entity.Health > 0)
                return;
            
            if (TryGetComponent(out BoxCollider boxCollider))
                boxCollider.enabled = false;
            if (TryGetComponent(out Rigidbody body))
                body.isKinematic = true;
            
            _entity.IsAlive = false;
            _entity.OnDeath?.Invoke();
        }

        private void ShowDamageNumber(float amount)
        {
            var clone = Instantiate(_damageNumberPrefab);
            clone.transform.position = transform.position;
            clone.Text = amount.ToString("0.#");
            clone.SetColor(GetComponent<Player>() != null ? Color.magenta : Color.red);
        }
    }
}