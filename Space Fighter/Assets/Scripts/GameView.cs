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
            m_mainMenuView.Initialize(this, new MainMenuController());
            m_mainMenuView.Show();

            m_playView.Initialize();
            m_playView.Hide();

            m_gameOverView.Initialize();
            m_gameOverView.Hide();
        }

        public void OnClickPlayButton()
        {
            m_mainMenuView.Hide();
        }
    }
}