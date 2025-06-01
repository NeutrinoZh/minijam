using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "HealthregenerationSpell", menuName = "Game/Spells/Health Regeneration")]
    public class HealthRegenerationSpell : Spell
    {
        [field: SerializeField] public List<float> HealthRegeneration { get; set; }
    }
}