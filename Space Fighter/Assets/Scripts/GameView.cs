using System.Collections;
using UnityEngine;

namespace JGM.Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private MainMenuView m_mainMenuView;
        [SerializeField] private PlayView m_playView;
        [SerializeField] private GameOverView m_gameOverView;

        public void Initialize()
        {
            m_mainMenuView.Initialize();
            m_playView.Initialize();
            m_gameOverView.Initialize();
        }
    }
}