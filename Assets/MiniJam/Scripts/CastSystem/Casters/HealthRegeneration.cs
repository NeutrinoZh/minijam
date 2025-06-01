using System.Linq;
using UnityEngine;
using Zenject;

namespace Game
{
    [SpellCaster(SpellType = typeof(HealthRegenerationSpell))]
    public class HealthRegenerationCaster : Caster
    {
        public float Heal =>
            (Level >= _spell.Regeneration.Count ? _spell.Regeneration.Last() : _spell.Regeneration[Level]) * PlayerInventory.GetSumOfBuff(nameof(Heal));

        private readonly HealthRegenerationSpell _spell;
        private readonly Entity _player;

        [Inject]
        public HealthRegenerationCaster(PlayerInventory inventory, HealthRegenerationSpell spell, Player player) :
            base(spell, inventory)
        {
            _spell = spell;
            _player = player.GetComponent<Entity>();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void CastInternal(Transform caster)
        {
            _player.Health += _player.MaxHealth * (Heal / 100);
            _player.Health = _player.Health > _player.MaxHealth ? _player.MaxHealth : _player.Health;
        }
    }
}