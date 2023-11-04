using UnityEngine;

namespace JGM.Game
{
    public class AsteroidPiecesSpawner : MonoBehaviour
    {
        [SerializeField] private AsteroidView m_asteroidToSubscribe;
        [SerializeField] private AsteroidView m_asteroidPiecePrefab;
        [SerializeField] private int m_amountOfPieces = 2;

        private void Awake()
        {
            m_asteroidToSubscribe.OnAsteroidBreak += SpawnAsteroidPieces;
        }

        private void SpawnAsteroidPieces()
        {
            for (int i = 0; i < m_amountOfPieces; i++)
            {
                var spawnedAsteroidPiece = Instantiate(m_asteroidPiecePrefab);
                bool startAsteroidMovingUp = i % 2 == 0;
                spawnedAsteroidPiece.Initialize(startAsteroidMovingUp, m_asteroidToSubscribe.transform.position, null);
            }
        }
    }
}