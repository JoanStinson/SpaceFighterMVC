using System;
using System.Threading.Tasks;
using UnityEngine;

namespace JGM.Game
{
    public class AsteroidPiecesSpawner : MonoBehaviour
    {
        [SerializeField] private AsteroidView m_asteroidToSubscribe;
        [SerializeField] private AsteroidView m_asteroidPiecePrefab;
        [SerializeField] private int m_amountOfPieces = 2;
        [SerializeField] private float m_delayToSpawnInSeconds = 1;

        private void Awake()
        {
            m_asteroidToSubscribe.OnAsteroidBreak += SpawnAsteroidPieces;
        }

        private async void SpawnAsteroidPieces()
        {
            await Task.Delay(TimeSpan.FromSeconds(m_delayToSpawnInSeconds));

            if (m_asteroidToSubscribe == null)
            {
                return;
            }

            for (int i = 0; i < m_amountOfPieces; i++)
            {
                var spawnedAsteroidPiece = Instantiate(m_asteroidPiecePrefab);
                spawnedAsteroidPiece.transform.SetParent(m_asteroidToSubscribe.transform.parent.parent);
                spawnedAsteroidPiece.Initialize(m_asteroidToSubscribe.transform.position, null, i % 2 == 0);
            }
        }
    }
}