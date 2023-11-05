using System;
using UnityEngine;

namespace JGM.Game
{
    [CreateAssetMenu(fileName = "New Game Settings", menuName = "Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public int initialScore => m_initialScore;
        public float maxHealth => m_maxHealth;
        public EnemySettings[] enemies => m_enemies;

        [Header("Play Settings")]
        [SerializeField] private int m_initialScore = 0;
        [SerializeField] private float m_maxHealth = 5;

        [Header("Enemies")]
        [SerializeField] private EnemySettings[] m_enemies;

        [Serializable]
        public class EnemySettings
        {
            public EnemyView enemyPrefab;
            [Range(1, 20)] public int poolSize = 10;
            [Range(1, 20)] public int spawnAmount = 5;
            [Range(0f, 30f)] public float delayBetweenSpawnsInSeconds = 5f;
        }
    }
}