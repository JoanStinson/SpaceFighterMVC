using UnityEngine;

namespace JGM.Game
{
    public abstract class ProjectileView : MonoBehaviour, IProjectile, IDamageable
    {
        public int scorePoints => 0;

        [SerializeField] protected float m_moveSpeed = 25f;
        [SerializeField] private float m_damagePower = 1f;
        [SerializeField] private float m_leftLimit = 9f;

        private ComponentPool<ProjectileView> m_pool;
        private GameModel m_gameModel;
        private bool m_launched;

        public void Launch(Transform mountPoint, ComponentPool<ProjectileView> pool, GameModel gameModel)
        {
            transform.position = mountPoint.position;
            m_pool = pool;
            m_gameModel = gameModel;
            m_launched = true;
        }

        public void TakeDamage(float damageAmount)
        {
            m_pool.Return(this);
            m_launched = false;
        }

        private void Update()
        {
            if (m_launched)
            {
                Move();
            }

            if (transform.position.x > m_leftLimit)
            {
                m_pool.Return(this);
            }
        }

        protected abstract void Move();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(m_damagePower);
                m_gameModel.score += damageable.scorePoints;
            }
        }
    }
}