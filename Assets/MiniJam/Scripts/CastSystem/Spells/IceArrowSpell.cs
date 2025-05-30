using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "IceArrowSpell", menuName = "Game/Spells/Ice Arrow")]
    public class IceArrowSpell : Spell
    {
        [field: SerializeField] public IceArrowProjectile ProjectilePrefab { get; private set; }
        [field: SerializeField] public List<float> Damage { get; private set; }
        [field: SerializeField] public List<float> Speed { get; private set; }
    }
}