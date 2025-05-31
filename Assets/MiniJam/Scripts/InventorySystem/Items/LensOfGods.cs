using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "LensOfGods", menuName = "Game/Items/Lens Of Gods")]
    public class LensOfGods : Item
    {
        [field: SerializeField] public List<float> Radius { get; private set; }
    }
}