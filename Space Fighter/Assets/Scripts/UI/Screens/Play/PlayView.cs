using System.ComponentModel;
using UnityEngine;

namespace JGM.Game
{
    public class PlayView : ScreenView
    {
        [Header("UI")]
        [SerializeField] private FillBarView m_healthBar;
        [SerializeField] private LocalizedText m_scoreText;

        [Header("Gameplay")]
        [SerializeField] private PlayerView m_player;
        [SerializeField] private EnemiesSpawner m_enemiesSpawner;

        private GameView m_gameView;
        private GameModel m_gameModel;

        public void Initialize(GameView gameView, GameModel gameModel)
        {
            m_gameView = gameView;
            m_gameModel = gameModel;
            m_gameModel.PropertyChanged += OnPropertyChanged;
            m_player.Initialize(m_gameView, m_gameModel);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var gameModel = (GameModel)sender;

            if (e.PropertyName == "score")
            {
                SetScoreText(gameModel.score);
            }
            else if (e.PropertyName == "currentHealth")
            {
                SetHealthBar(gameModel);
            }
        }

        public override void Show()
        {
            base.Show();
            SetScoreText(m_gameModel.score);
            SetHealthBar(m_gameModel);
            m_player.gameObject.SetActive(true);
            m_enemiesSpawner.Spawn();
        }

        public override void Hide()
        {
            base.Hide();
            m_player.gameObject.SetActive(false);
        }

        private void SetHealthBar(GameModel gameModel)
        {
            m_healthBar.SetValue(gameModel.currentHealth, gameModel.maxHealth);
        }

        private void SetScoreText(int score)
        {
            m_scoreText.LocaliseText("TID_SCORE", $" {score}");
        }
    }
}