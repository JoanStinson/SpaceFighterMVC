namespace JGM.Game
{
    public class GameController
    {
        private GameModel m_gameModel;

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public GameModel GetGameModel(GameSettings gameSettings)
        {
            if (m_gameModel == null)
            {
                m_gameModel = new GameModel();
                m_gameModel.score = gameSettings.initialScore;
                m_gameModel.currentHealth = gameSettings.maxHealth;
                m_gameModel.maxHealth = gameSettings.maxHealth;
            }

            return m_gameModel;
        }
    }
}