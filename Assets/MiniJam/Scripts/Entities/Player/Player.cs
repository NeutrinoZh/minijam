using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private List<Spell> _spells;
        private readonly List<Caster> _casters = new();
        
        private CastersRegister _castersRegister;

        [Inject]
        public void Construct(CastersRegister casterRegister)
        {
            _castersRegister = casterRegister;
        }
        
        private void Awake()
        {
            foreach (var spell in _spells)
                _casters.Add(_castersRegister.CreateNewCasterForSpell(spell.GetType()));
            
            foreach (var caster in _casters) 
                StartCoroutine(StartCasting(caster));
        }
        
        private IEnumerator StartCasting(Caster caster)
        {
            while (true)
            {
                yield return new WaitForSeconds(caster.Cooldown);
                caster.Cast(transform);
            }
        }
    }
}