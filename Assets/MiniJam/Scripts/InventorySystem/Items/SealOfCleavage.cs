using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SealOfCleavage", menuName = "Game/Items/Seal Of Cleavage")]
    public class SealOfCleavage : Item
    {
        [field: SerializeField] public List<float> Projectiles { get; private set; }
    }
}