using UnityEngine;
using Zenject;

namespace Game
{
    public class Enemy : MonoBehaviour
    {
        private ListOfObject<Enemy> _enemies;
        
        [Inject]
        public void Construct(ListOfObject<Enemy> enemies)
        {   
            _enemies = enemies;
            enemies.Objects.Add(this);
        }

        private void OnDestroy()
        {
            _enemies.Objects.Remove(this);
        }
    }
}