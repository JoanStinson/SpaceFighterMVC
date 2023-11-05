using UnityEngine;

namespace JGM.Game
{
    public class MissileLauncher : MonoBehaviour, ILauncher
    {
        [SerializeField, Range(1, 50)]
        private int m_missilesPoolSize = 20;

        [SerializeField] private Transform m_missilesPoolParent;
        [SerializeField] private MissileView m_missilePrefab;

        private GameModel m_gameModel;
        private ComponentPool<ProjectileView> m_missilesPool;

        public void Initialize(GameModel gameModel)
        {
            m_gameModel = gameModel;
            m_missilesPool = new ComponentPool<ProjectileView>(m_missilesPoolSize, m_missilesPoolParent, m_missilePrefab);
        }

        public void Launch(PlayerWeapon weapon)
        {
            var spawnedMissile = m_missilesPool.Get();
            //TODO change this rubbish code
            var target = GameObject.FindObjectOfType<EnemyView>();
            (spawnedMissile as MissileView).SetTarget(target.transform);
            spawnedMissile?.Launch(weapon.weaponMountPoint, m_missilesPool, m_gameModel);
        }
    }
}