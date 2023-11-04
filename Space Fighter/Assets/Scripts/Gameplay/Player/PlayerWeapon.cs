using UnityEngine;

namespace JGM.Game
{
    public class PlayerWeapon : MonoBehaviour
    {
        public Transform weaponMountPoint => m_weaponMountPoint;
        
        [SerializeField] private float m_fireWeaponRefreshRate = 0.25f;
        [SerializeField] private Transform m_weaponMountPoint;

        private float m_nextFireTime;

        public void FireWeapon(ILauncher launcher)
        {
            if (!CanFire())
            {
                return;
            }

            m_nextFireTime = Time.time + m_fireWeaponRefreshRate;
            launcher?.Launch(this);
        }

        private bool CanFire()
        {
            return Time.time >= m_nextFireTime;
        }
    }
}