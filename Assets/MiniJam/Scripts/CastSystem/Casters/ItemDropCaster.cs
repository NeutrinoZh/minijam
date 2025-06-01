using UnityEngine;
using Zenject;

namespace Game
{
    [SpellCaster(SpellType = typeof(ItemDropSpell))]
    public class ItemDropCaster : Caster
    {
        private readonly ItemSelectionScreen _itemSelectionScreen;

        [Inject]
        public ItemDropCaster(ItemDropSpell spell, PlayerInventory inventory, ItemSelectionScreen itemSelectionScreen) :
            base(spell, inventory)
        {
            _itemSelectionScreen = itemSelectionScreen;
        }
        
        
        // ReSharper disable Unity.PerformanceAnalysis
        protected override void CastInternal(Transform caster)
        {
            _itemSelectionScreen.Show();
        }
    }
}