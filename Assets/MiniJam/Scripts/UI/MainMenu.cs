using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _exitGameButton;
        
        private void Start()
        {
            _startGameButton.onClick.AddListener(HandleStartGame);
            _exitGameButton.onClick.AddListener(HandleExitGame);
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveAllListeners();
            _exitGameButton.onClick.RemoveAllListeners();
        }

        private static void HandleStartGame()
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }

        private static void HandleExitGame()
        {
            Application.Quit();
        }
    }
}