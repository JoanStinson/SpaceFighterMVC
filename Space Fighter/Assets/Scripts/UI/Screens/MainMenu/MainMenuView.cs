using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class MainMenuView : ScreenView
    {
        [SerializeField] private Button m_playButton;
        [SerializeField] private Button m_quitButton;
        [SerializeField] private Button m_languageButton;

        private GameView m_gameView;
        private MainMenuController m_controller;

        public void Initialize(GameView gameView, MainMenuController controller)
        {
            m_gameView = gameView;
            m_controller = controller;

            m_playButton.onClick.AddListener(OnClickPlayButton);
            m_quitButton.onClick.AddListener(OnClickQuitButton);
            m_languageButton.onClick.AddListener(OnClickLanguageButton);
        }

        private void OnClickPlayButton()
        {
            m_gameView.OnClickPlayButton();
        }

        private void OnClickQuitButton()
        {
            m_controller.QuitGame();
        }

        private void OnClickLanguageButton()
        {
            m_controller.ChangeLanguageRandomly();
        }
    }
}