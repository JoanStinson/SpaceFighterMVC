using System.ComponentModel;

namespace JGM.Game
{
    public class GameModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int m_score;
        public int score 
        { 
            get => m_score;
            set
            {
                if (m_score != value)
                {
                    m_score = value;
                    OnPropertyChanged("score");
                }
            }
        }

        private float m_currentHealth;
        public float currentHealth
        {
            get => m_currentHealth;
            set
            {
                if (m_currentHealth != value)
                {
                    m_currentHealth = value;
                    OnPropertyChanged("currentHealth");
                }
            }
        }

        public float maxHealth { get; set; }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}