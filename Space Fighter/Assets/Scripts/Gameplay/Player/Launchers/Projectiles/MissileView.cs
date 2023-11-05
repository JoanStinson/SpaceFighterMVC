using UnityEngine;

namespace JGM.Game
{
    public class MissileView : ProjectileView
    {
        private Transform m_target;

        public void SetTarget(Transform target)
        {
            m_target = target;
        }

        protected override void Move()
        {
            if (m_target == null)
            {
                TakeDamage(1);
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, m_target.position, m_moveSpeed * Time.deltaTime);
        }
    }
}