using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField] private Image _itemIcon;

        public void SetData(Item item)
        {
            _itemIcon.sprite = item.Icon;
        }
    }
}