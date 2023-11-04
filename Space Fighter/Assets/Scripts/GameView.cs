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

        public void Initialize()
        {
            m_mainMenuView.Initialize(this, new MainMenuController(m_localizationService));
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