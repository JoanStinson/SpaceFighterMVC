namespace JGM.Game
{
    public class GameController
    {
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public GameModel GetGameModel()
        {
            return new GameModel();
        }
    }
}