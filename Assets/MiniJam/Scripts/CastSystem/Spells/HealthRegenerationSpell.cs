using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "HealthRegenerationSpell", menuName = "Game/Spells/Health Regeneration")]
    public class HealthRegenerationSpell : Spell
    {
        [field: SerializeField] public List<float> Regeneration { get; set; }
    }
}