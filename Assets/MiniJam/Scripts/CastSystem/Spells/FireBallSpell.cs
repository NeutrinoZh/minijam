using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "FireBallSpell", menuName = "Game/Spells/Fire Ball")]
    public class FireBallSpell : Spell
    {
        [field: SerializeField] public FireBallProjectile ProjectilePrefab { get; private set; }
        [field: SerializeField] public List<float> Damage { get; private set; }
        [field: SerializeField] public List<float> Speed { get; private set; }
    }
}