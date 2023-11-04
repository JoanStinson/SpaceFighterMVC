using UnityEngine;

namespace JGM.Game
{
    public class BulletView : MonoBehaviour, IProjectile
    {
        [SerializeField] 
        private float m_moveSpeed = 25f;

        private ComponentPool<BulletView> m_pool;
        private bool m_launched;

        public void Launch(Transform mountPoint, ComponentPool<BulletView> pool)
        {
            transform.position = mountPoint.position;
            m_pool = pool;
            m_launched = true;
        }

        private void Update()
        {
            if (!m_launched)
            {
                return;
            }

            transform.position += Vector3.right * m_moveSpeed * Time.deltaTime;

            if (transform.position.x > 9f)
            {
                m_pool.Return(this);
            }
        }
    }
}