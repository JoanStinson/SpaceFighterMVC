using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private MainMenuView m_mainMenuView;
        [SerializeField] private PlayView m_playView;
        [SerializeField] private GameOverView m_gameOverView;

        [Inject] private ILocalizationService m_localizationService;
        [Inject] private GameSettings m_gameSettings;

        private GameController m_gameController;
        private GameModel m_gameModel;

        public void Initialize(GameController gameController)
        {
            m_gameController = gameController;
            m_gameModel = m_gameController.GetGameModel(m_gameSettings);

            m_mainMenuView.Initialize(this, new MainMenuController(m_localizationService));
            m_mainMenuView.Show();

            m_playView.Initialize(this, m_gameModel);
            m_playView.Hide();

            m_gameOverView.Initialize(this, m_gameModel);
            m_gameOverView.Hide();
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
            m_gameModel.Reset();
        }

        public async void OnPlayerKilled()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            m_playView.Hide();
            m_gameOverView.Show();
        }
    }
}