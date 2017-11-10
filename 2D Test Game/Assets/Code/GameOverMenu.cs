using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpaceShooter.States;

namespace SpaceShooter
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField]
        private GameStateType _nextState;
        [SerializeField]
        private GameObject _winText;
        [SerializeField]
        private GameObject _defeatText;

        public void MainMenu()
        {
            GameStateController.PerformTransition(_nextState);
        }

        private void Awake()
        {
            if (GameManager.Instance.playerWon)
            {
                _winText.SetActive(true);
            }
            else
            {
                _defeatText.SetActive(true);
            }
        }
    }
}