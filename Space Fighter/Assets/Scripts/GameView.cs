using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private MainMenuView m_mainMenuView;
        [SerializeField] private PlayView m_playView;
        [SerializeField] private GameOverView m_gameOverView;

        [Inject]
        private ILocalizationService m_localizationService;
        private GameController m_gameController;

        public void Initialize(GameController gameController)
        {
            m_gameController = gameController;

            m_mainMenuView.Initialize(this, new MainMenuController(m_localizationService));
            m_mainMenuView.Hide();

            m_playView.Initialize();
            m_playView.Hide();

            m_gameOverView.Initialize(this, m_gameController.GetGameModel());
            m_gameOverView.Show();
        }

        public void OnClickPlayButton()
        {
            m_mainMenuView.Hide();
            m_playView.Show();
        }

        public void OnClickQuitButton()
        {
            m_gameController.QuitGame();
        }

        public void OnClickRetryButton()
        {
            m_gameOverView.Hide();
            m_playView.Show();
        }
    }
}