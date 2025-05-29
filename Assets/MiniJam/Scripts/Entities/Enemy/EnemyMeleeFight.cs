using System.Collections;
using UnityEngine;
using Zenject;

namespace Game
{
    public class EnemyMeleeFight : MonoBehaviour
    {
        private Entity _entity;
        private Player _player;
        private EntityDamagable _damagable;
        
        private bool _attacking = false;
        
        [Inject]
        public void Construct(Player player)
        {
            _player = player;
            _entity = GetComponent<Entity>();
            _damagable = _player.GetComponent<EntityDamagable>();
        }

        private void Update()
        {
            if (_attacking || !_player)
                return;
            
            var offset = _player.transform.position - transform.position;
            if (!(offset.magnitude < _entity.RangeOfAttack))
                return;
            
            _attacking = true;
            _entity.CanMove = false;
                
            StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            yield return new WaitForSeconds(Entity.BaseAttackDuration / _entity.AttackSpeed);
            _attacking = false;
            _entity.CanMove = true;
            _damagable.Damage(_entity.Attack);
        }
    }
}