using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using TMPro;
using UnityEngine;

namespace Game
{
    public class DamageNumber : MonoBehaviour
    {
        [SerializeField] private float _offset;
        [SerializeField] private float _duration;
        
        private TextMeshProUGUI _textMesh;
        private Camera _camera;

        public string Text
        {
            get => _textMesh.text;
            set => _textMesh.text = value;
        }
        
        private void Awake()
        {
            _camera = Camera.main;
            _textMesh = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            Tween.PositionY(_textMesh.transform, transform.position.y + _offset, _duration);
        }

        private void Update()
        {
            transform.forward = _camera.transform.forward;
        }
    }
}