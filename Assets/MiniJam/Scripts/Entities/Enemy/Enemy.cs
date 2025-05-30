using UnityEngine;
using Zenject;

namespace Game
{
    public class Enemy : MonoBehaviour
    {
        private ListOfObject<Enemy> _enemies;
        private PoolOfObject<Experience> _experiencePool;
        private Entity _entity;
        
        [Inject]
        public void Construct(ListOfObject<Enemy> enemies, PoolOfObject<Experience> experiencePool)
        {   
            _experiencePool = experiencePool;
            _enemies = enemies;
            _entity = GetComponent<Entity>();
            
            enemies.Objects.Add(this);
        }

        private void Start()
        {
            _entity.OnDeath += SpawnExperience;
        }

        private void OnDestroy()
        {
            _entity.OnDeath -= SpawnExperience;
            _enemies.Objects.Remove(this);
        }
        
        private void SpawnExperience()
        {
            var clone = _experiencePool.Instantiate();
            clone.transform.position = transform.position;   
        }
    }
}