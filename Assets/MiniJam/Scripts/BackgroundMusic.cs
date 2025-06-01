using UnityEngine;

namespace Game
{
    public class BackgroundMusic : MonoBehaviour
    {
        private static BackgroundMusic _instance = null;
        
        private void Start()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            _instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
    }
}