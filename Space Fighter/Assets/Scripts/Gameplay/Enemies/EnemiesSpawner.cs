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
        private readonly List<ComponentPool<EnemyView>> m_enemyPools = new List<ComponentPool<EnemyView>>();
        private bool m_spawn;

        public void Spawn(GameModel gameModel)
        {
            m_gameModel = gameModel;
            CreateEnemyPools(gameModel);
            m_spawn = true;
        }

        private void CreateEnemyPools(GameModel gameModel)
        {
            foreach (var setting in gameModel.enemySettings)
            {
                var poolParent = new GameObject("EnemyPool").transform;
                poolParent.SetParent(transform);
                var newEnemyPool = new ComponentPool<EnemyView>(setting.poolSize, poolParent, setting.enemyPrefab);
                m_enemyPools.Add(newEnemyPool);
            }
        }

        private async void Update()
        {
            if (!m_spawn)
            {
                return;
            }

            await Task.Delay(TimeSpan.FromSeconds(m_delayBetweenSpawnsInSeconds));
            SpawnEnemies();
        }

        private void SpawnEnemies()
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
                }
            }
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
        }
    }
}