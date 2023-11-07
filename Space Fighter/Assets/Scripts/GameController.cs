using UnityEngine;

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
            m_gameModel ??= new GameModel(gameSettings.initialScore, gameSettings.maxHealth, gameSettings.enemies);
            return m_gameModel;
        }
    }
}