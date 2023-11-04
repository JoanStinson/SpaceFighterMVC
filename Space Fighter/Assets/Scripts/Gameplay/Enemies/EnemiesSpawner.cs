using UnityEngine;

namespace JGM.Game
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [Header("Asteroid")]
        [SerializeField] private int m_asteroidsPoolSize;
        [SerializeField] private Transform m_asteroidsPoolParent;
        [SerializeField] private AsteroidView m_asteroidPrefab;
        [SerializeField] private int m_asteroidsAmount;

        //[Header("Asteroid Piece")]
        //[SerializeField] private int m_asteroidPiecesPoolSize;
        //[SerializeField] private Transform m_asteroidsPiecesPoolParent;
        //[SerializeField] private AsteroidView m_asteroidPiecePrefab;

        public void Spawn()
        {
            var asteroidsPool = new ComponentPool<AsteroidView>(m_asteroidsPoolSize, m_asteroidsPoolParent, m_asteroidPrefab);
            //var asteroidPiecesPool = new ComponentPool<AsteroidView>(m_asteroidPiecesPoolSize, m_asteroidsPiecesPoolParent, m_asteroidPiecePrefab);

            for (int i = 0; i < m_asteroidsAmount; i++)
            {
                var spawnedAsteroid = asteroidsPool.Get();
                spawnedAsteroid.Initialize(i % 2 == 0, Vector3.zero, asteroidsPool);
            }
        }
    }
}