using UnityEngine;

namespace JGM.Game
{
    [CreateAssetMenu(fileName = "New Game Settings", menuName = "Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public int initialScore => m_initialScore;
        public float maxHealth => m_maxHealth;

        [Header("Play Settings")]
        [SerializeField] private int m_initialScore = 0;
        [SerializeField] private float m_maxHealth = 5;

        //[Header("Enemies")]

    }
}