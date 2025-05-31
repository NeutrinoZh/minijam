using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "RingOfArcane", menuName = "Game/Items/Ring Of Arcane")]
    public class RingOfArcane : Item
    {
        [field: SerializeField] public List<float> Cooldown { get; private set; }
    }
}