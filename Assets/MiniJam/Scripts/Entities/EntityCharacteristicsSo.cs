using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "New Entity", menuName = "Game/Entity")]
    public class EntityCharacteristicsSo : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float RangeOfAttack { get; private set; }
        [field: SerializeField] public float AttackSpeed { get; private set; }
        [field: SerializeField] public float Attack { get; private set; }
        [field: SerializeField] public float MaxHealth { get; private set; }
    }
}