using System;

namespace JGM.Game
{
    public class MainMenuController
    {
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void ChangeLanguageRandomly()
        {
            throw new NotImplementedException();
        }
    }
}