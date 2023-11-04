using static JGM.Game.LocalizationService;
using Random = UnityEngine.Random;

namespace JGM.Game
{
    public class MainMenuController
    {
        private readonly ILocalizationService m_localizationService;

        public MainMenuController(ILocalizationService localizationService)
        {
            m_localizationService = localizationService;
        }

        public void ChangeLanguageRandomly()
        {
            Language currentLanguage = m_localizationService.currentLanguage;
            Language randomLanguage;

            do
            {
                randomLanguage = (Language)Random.Range(0, (int)Language.Count);
            }
            while (randomLanguage == currentLanguage);

            m_localizationService.SetLanguage(randomLanguage);
        }
    }
}