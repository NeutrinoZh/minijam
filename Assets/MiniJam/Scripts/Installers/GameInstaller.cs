using Game.InventorySystem;
using UnityEngine;
using Zenject;

namespace Game {
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private Experience _experiencePrefab;
        [SerializeField] private ItemSelectionScreen _itemSelectionScreen;
        
        public override void InstallBindings()
        {
            Container.Bind<ItemSelectionScreen>().FromInstance(_itemSelectionScreen).AsSingle();
            
            Container.Bind<ListOfObject<Enemy>>().AsSingle();
            Container.Bind<ListOfObject<Projectile>>().AsSingle();
            Container.Bind<PoolOfObject<Experience>>().FromInstance(
                new PoolOfObject<Experience>(Container, _experiencePrefab)).AsSingle();
            
            Container.Bind<CastersRegister>().AsSingle();
            Container.Bind<ItemsRegister>().AsSingle();
            Container.Bind<PlayerInventory>().AsSingle();
            
            Container.Bind<Controls>().AsSingle();
            
            Container.Bind<Player>().FromInstance(_player).AsSingle();
        }
    }
}