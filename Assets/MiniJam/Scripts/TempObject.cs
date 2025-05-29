using System.Collections;
using UnityEngine;

namespace Game
{
    public class TempObject : MonoBehaviour
    {
        [SerializeField] private float _timeOfLife;

        public float TimeOfLife
        {
            get => _timeOfLife;
            set
            {
                _timeOfLife = value;
                StopAllCoroutines();
                StartCoroutine(StartTimer());
            }
        }
        
        private void Start()
        {
            if (_timeOfLife != 0)
                StartCoroutine(StartTimer());
        }

        private IEnumerator StartTimer()
        {
            yield return new WaitForSeconds(_timeOfLife);
            Destroy(gameObject);
        }
    }
}