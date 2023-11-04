using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class GameOverView : ScreenView
    {
        [SerializeField] private LocalizedText m_scoreText;
        [SerializeField] private Button m_retryButton;
        [SerializeField] private Button m_quitButton;
        
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
        }

        private void OnClickQuitButton()
        {
            m_gameView.OnClickQuitButton();                  
        }

        public override void Show()
        {
            base.Show();
            m_scoreText.LocaliseText("TID_SCORE", $" {m_gameModel.score}");
        }
    }
}