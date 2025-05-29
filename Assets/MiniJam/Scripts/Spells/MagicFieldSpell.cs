using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    [CreateAssetMenu(fileName = "MagicFieldSpell", menuName = "Game/Spells/Magic Field")]
    public class MagicFieldSpell : Spell
    {
        [field: SerializeField] public MagicFieldProjectile ProjectilePrefab { get; private set; }
        
        [field: SerializeField] public List<float> Duration { get; private set; }
        [field: SerializeField] public List<float> Radius { get; private set; }
        [field: SerializeField] public List<float> Damage { get; private set; }
        
       
    }
}