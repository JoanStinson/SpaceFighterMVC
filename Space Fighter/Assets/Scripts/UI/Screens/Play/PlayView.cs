using System.ComponentModel;
using UnityEngine;

namespace JGM.Game
{
    public class PlayView : ScreenView
    {
        [Header("UI")]
        [SerializeField] private FillBarView m_healthBar;
        [SerializeField] private TextMeshProAnimatedBinder m_scoreText;

        [Header("Gameplay")]
        [SerializeField] private PlayerView m_player;
        [SerializeField] private EnemiesSpawner m_enemiesSpawner;

        private GameView m_gameView;
        private GameModel m_gameModel;
        private string m_scoreName;

        public void Initialize(GameView gameView, GameModel gameModel, ILocalizationService localizationService)
        {
            m_gameView = gameView;
            m_gameModel = gameModel;
            m_gameModel.PropertyChanged += OnPropertyChanged;
            m_player.Initialize(m_gameView, m_gameModel);
            m_scoreName = $"{localizationService.Localize("TID_SCORE")} ";
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var gameModel = (GameModel)sender;

            if (e.PropertyName == "score")
            {
                m_scoreText.SetValueAnimated(gameModel.score, m_scoreName);
            }
            else if (e.PropertyName == "currentHealth")
            {
                SetHealthBar(gameModel);
            }
        }

        public override void Show()
        {
            base.Show();
            m_scoreText.SetValue(m_gameModel.score, m_scoreName);
            SetHealthBar(m_gameModel);
            m_player.gameObject.SetActive(true);
            m_enemiesSpawner.Spawn();
        }

        public override void Hide()
        {
            base.Hide();
            m_player.gameObject.SetActive(false);
            m_enemiesSpawner.Return();
        }

        private void SetHealthBar(GameModel gameModel)
        {
            m_healthBar.SetValue(gameModel.currentHealth, gameModel.maxHealth);
        }
    }
}