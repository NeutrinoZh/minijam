using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using TMPro;
using UnityEngine;

namespace Game
{
    public class FriendDialogs : MonoBehaviour
    {
        [SerializeField] private List<string> _texts;
        [SerializeField] private TextMeshProUGUI _textLabel;
        [SerializeField] private float _interval;
        
        private Camera _camera;
        
        private void Start()
        {
            _camera = Camera.main;
            StartCoroutine(StartDialogs());
        }
        
        private void Update()
        {
            _textLabel.transform.forward = _camera.transform.forward;
        }
        
        private IEnumerator StartDialogs()
        {
            yield return new WaitForSeconds(3);
            
            while (true)
            {
                _textLabel.text = _texts[Random.Range(0, _texts.Count)];
                // Tween.Custom(
                //     0, 1, 1,
                //     value => _textLabel.alpha = value
                //     ).OnComplete(() =>
                // {
                //     Tween.Custom(
                //         1, 0, 1,
                //         value => _textLabel.alpha = value,
                //         startDelay: 3);
                // });
                
                yield return new WaitForSeconds(_interval);
            }
        }
    }
}