using UnityEngine;
using Zenject;

namespace Game
{
    public class Projectile : MonoBehaviour
    {
        [field: SerializeField] public Team Team { get; private set; }
        
        private ListOfObject<Projectile>_projectiles;
        
        [Inject]
        public void Construct(ListOfObject<Projectile> projectiles)
        {
            _projectiles = projectiles;
            projectiles.Objects.Add(this);
        }

        private void OnDestroy()
        {
            _projectiles.Objects.Remove(this);
        }
    }
}