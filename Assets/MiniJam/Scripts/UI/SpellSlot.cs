using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class SpellSlot : MonoBehaviour
    {
        [SerializeField] private Image _spellIcon;
        [SerializeField] private Image _cooldownMask;

        private Caster _caster;
        
        public void SetData(Caster caster)
        {
            _caster = caster;
            _spellIcon.sprite = _caster.Spell.Icon;
            _caster.OnCast += CastHandle;   
        }

        private void OnDestroy()
        {
            if (_caster != null)
                _caster.OnCast -= CastHandle;
        }
        
        private void CastHandle()
        {
            _cooldownMask.fillAmount = 1;
            Tween.UIFillAmount(_cooldownMask, 0.0f, _caster.Cooldown);
        }
    }
}