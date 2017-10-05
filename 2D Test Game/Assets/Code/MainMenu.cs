using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private string _LevelName;
        
        public void StartGame()
        {
            SceneManager.LoadScene(_LevelName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }
}