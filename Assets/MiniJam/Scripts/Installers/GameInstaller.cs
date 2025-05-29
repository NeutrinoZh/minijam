using UnityEngine;
using Zenject;

namespace Game {
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        
        public override void InstallBindings()
        {
            Container.Bind<ListOfObject<Enemy>>().AsSingle();
            Container.Bind<ListOfObject<Projectile>>().AsSingle();
            
            Container.Bind<CastersRegister>().AsSingle();
            
            Container.Bind<Controls>().AsSingle();
            Container.Bind<Player>().FromInstance(_player).AsSingle();
        }
    }
}