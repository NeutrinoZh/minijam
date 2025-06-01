using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Game
{
    public class TimerLabel : MonoBehaviour
    {
        private TextMeshProUGUI _timerLabel;
        
        private void Awake()
        {
            _timerLabel = GetComponent<TextMeshProUGUI>();
        }
        
        private void Start()
        {
            StartCoroutine(UpdateTimer());
        }
        
        private IEnumerator UpdateTimer()
        {
            while (true)
            {
                var time = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
                _timerLabel.text = time.ToString(@"mm\:ss");
                yield return new WaitForSeconds(1);
            }
        }
    }
}