using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "RingOfPower", menuName = "Game/Items/Ring Of Power")]
    public class RingOfPower : Item
    {
        [field: SerializeField] public List<float> Damage { get; private set; }
    }
}