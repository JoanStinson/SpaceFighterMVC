using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JGM.Game
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField, Range(0f, 30f)]
        private float m_delayBetweenSpawnsInSeconds = 5f;

        [SerializeField] private float m_bottomLimitSpawn = -3.85f;
        [SerializeField] private float m_topLimitSpawn = 3.85f;
        [SerializeField] private float m_leftLimitSpawn = 9f;
        [SerializeField] private float m_rightLimitSpawn = 22f;

        private GameModel m_gameModel;
        private List<ComponentPool<EnemyView>> m_enemyPools;
        private bool m_spawnActive;

        public void Spawn(GameModel gameModel)
        {
            m_gameModel = gameModel;
            CreateEnemyPools(gameModel);
            m_spawnActive = true;
        }

        private void CreateEnemyPools(GameModel gameModel)
        {
            if (m_enemyPools != null)
            {
                return;
            }

            m_enemyPools = new List<ComponentPool<EnemyView>>();
            foreach (var setting in gameModel.enemySettings)
            {
                var poolParent = new GameObject("EnemyPool").transform;
                poolParent.SetParent(transform);
                var newEnemyPool = new ComponentPool<EnemyView>(setting.poolSize, poolParent, setting.enemyPrefab);
                m_enemyPools.Add(newEnemyPool);
            }
        }

        private void Update()
        {
            if (m_spawnActive)
            {
                SpawnEnemies();
            }
        }

        private async void SpawnEnemies()
        {
            for (int i = 0; i < m_gameModel.enemySettings.Length; i++)
            {
                var enemyPool = m_enemyPools[i];

                for (int j = 0; j < m_gameModel.enemySettings[i].spawnAmount; j++)
                {
                    var enemy = enemyPool.Get();

                    if (enemy != null)
                    {
                        enemy.Initialize(GetRandomStartPosition(), enemyPool, j % 2 == 0);
                    }
                    
                    await Task.Delay(TimeSpan.FromSeconds(m_gameModel.enemySettings[i].delayBetweenSpawnsInSeconds));
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(m_delayBetweenSpawnsInSeconds));
        }

        private Vector2 GetRandomStartPosition()
        {
            float xRandomPos = Random.Range(m_leftLimitSpawn, m_rightLimitSpawn);
            float yRandomPos = Random.Range(m_bottomLimitSpawn, m_topLimitSpawn);
            return new Vector2(xRandomPos, yRandomPos);
        }

        public void Return()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var poolParent = transform.GetChild(i);
                foreach (Transform item in poolParent)
                {
                    if (item.TryGetComponent<EnemyView>(out var enemyView))
                    {
                        enemyView.Return();
                    }
                }
            }

            m_spawnActive = false;
        }
    }
}