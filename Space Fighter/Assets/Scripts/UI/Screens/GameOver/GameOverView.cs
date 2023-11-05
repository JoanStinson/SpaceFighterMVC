using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JGM.Game
{
    public class GameOverView : ScreenView
    {
        [SerializeField] private LocalizedText m_scoreText;
        [SerializeField] private Button m_retryButton;
        [SerializeField] private Button m_quitButton;

        [Inject]
        private IAudioService m_audioService;
        private GameView m_gameView;
        private GameModel m_gameModel;

        public void Initialize(GameView gameView, GameModel gameModel)
        {
            m_gameView = gameView;
            m_gameModel = gameModel;

            m_retryButton.onClick.AddListener(OnClickRetryButton);
            m_quitButton.onClick.AddListener(OnClickQuitButton);
        }

        private void OnClickRetryButton()
        {
            m_gameView.OnClickRetryButton();
            m_audioService.Play(AudioFileNames.buttonClickSfx);
        }

        private void OnClickQuitButton()
        {
            m_gameView.OnClickQuitButton();
            m_audioService.Play(AudioFileNames.buttonClickSfx);
        }

        public override void Show()
        {
            base.Show();
            m_scoreText.LocaliseText("TID_SCORE", $" {m_gameModel.score}");
        }
    }
}