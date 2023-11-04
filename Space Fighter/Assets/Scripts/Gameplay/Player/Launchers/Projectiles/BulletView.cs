using UnityEngine;

namespace JGM.Game
{
    public class BulletView : MonoBehaviour, IProjectile, IDamageable
    {
        [SerializeField] private float m_moveSpeed = 25f;
        [SerializeField] private float m_damagePower = 1f;

        private ComponentPool<BulletView> m_pool;
        private GameModel m_gameModel;
        private bool m_launched;

        public void Launch(Transform mountPoint, ComponentPool<BulletView> pool, GameModel gameModel)
        {
            transform.position = mountPoint.position;
            m_pool = pool;
            m_gameModel = gameModel;
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

        public void TakeDamage(float damageAmount)
        {
            m_pool.Return(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(m_damagePower);
                m_gameModel.score += 100;
            }
        }
    }
}