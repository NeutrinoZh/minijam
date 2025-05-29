using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class Spell : ScriptableObject
    {
        [field: SerializeField] public int MaxLevel { get; private set; }
        [field: SerializeField] public List<float> Cooldown { get; private set; } 
    }
}