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
        [SerializeField] private Transform m_gameplayView;
        [SerializeField] private PlayerView m_player;
        [SerializeField] private EnemiesSpawner m_enemiesSpawner;

        private GameView m_gameView;
        private GameModel m_gameModel;
        private ILocalizationService m_localizatioService;

        public void Initialize(GameView gameView, GameModel gameModel, ILocalizationService localizationService)
        {
            m_gameView = gameView;
            m_gameModel = gameModel;
            m_gameModel.PropertyChanged += OnPropertyChanged;
            m_localizatioService = localizationService;
            m_player.Initialize(m_gameView, m_gameModel);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var gameModel = (GameModel)sender;

            if (e.PropertyName == "score")
            {
                m_scoreText.SetValueAnimated(gameModel.score, $"{m_localizatioService.Localize("TID_SCORE")} ");
            }
            else if (e.PropertyName == "currentHealth")
            {
                SetHealthBar(gameModel);
            }
        }

        public override void Show()
        {
            base.Show();
            m_gameplayView.gameObject.SetActive(true);
            m_scoreText.SetValue(m_gameModel.score, $"{m_localizatioService.Localize("TID_SCORE")} ");
            SetHealthBar(m_gameModel);
            m_player.Show();
            m_enemiesSpawner.Spawn(m_gameModel);
        }

        public override void Hide()
        {
            base.Hide();
            m_gameplayView.gameObject.SetActive(false);
            m_player.Hide();
            m_enemiesSpawner.Return();
        }

        private void SetHealthBar(GameModel gameModel)
        {
            m_healthBar.SetValue(gameModel.currentHealth, gameModel.maxHealth);
        }
    }
}