using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JGM.Game
{
    public class MainMenuView : ScreenView
    {
        [SerializeField] private Button m_playButton;
        [SerializeField] private Button m_quitButton;
        [SerializeField] private Button m_languageButton;

        [Inject]
        private IAudioService m_audioService;
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
            m_audioService.Play(AudioFileNames.buttonClickSfx);
        }

        private void OnClickQuitButton()
        {
            m_gameView.OnClickQuitButton();
            m_audioService.Play(AudioFileNames.buttonClickSfx);
        }

        private void OnClickLanguageButton()
        {
            m_controller.ChangeLanguageRandomly();
            m_audioService.Play(AudioFileNames.buttonClickSfx);
        }
    }
}