using UnityEngine;

namespace JGM.Game
{
    public class BulletLauncher : MonoBehaviour, ILauncher
    {
        [SerializeField, Range(1, 50)]
        private int m_bulletsPoolSize = 20;

        [SerializeField] private Transform m_bulletsPoolParent;
        [SerializeField] private BulletView m_bulletPrefab;

        private ComponentPool<BulletView> m_bulletsPool;

        private void Awake()
        {
            m_bulletsPool = new ComponentPool<BulletView>(m_bulletsPoolSize, m_bulletsPoolParent, m_bulletPrefab);
        }

        public void Launch(PlayerWeapon weapon)
        {
            var spawnedBullet = m_bulletsPool.Get();
            spawnedBullet?.Launch(weapon.weaponMountPoint, m_bulletsPool);
        }
    }
}