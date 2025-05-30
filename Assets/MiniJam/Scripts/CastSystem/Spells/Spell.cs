using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class Spell : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        
        [field: SerializeField] public int MaxLevel { get; private set; }
        [field: SerializeField] public List<float> Cooldown { get; private set; }
    }
}