using UnityEngine;

namespace Game
{
    public abstract class Item : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public int MaxLevel { get; private set; }
    }
}