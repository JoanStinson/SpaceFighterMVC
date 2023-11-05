using UnityEngine;

namespace JGM.Game
{
    public class EnemyShipView : EnemyView
    {
        [Header("Enemy Ship Specifics")]
        [SerializeField] private Transform m_thruster;

        public override void Initialize(Vector3 startPosition, ComponentPool<EnemyView> pool, bool startMovingUp)
        {
            base.Initialize(startPosition, pool, startMovingUp);
            m_thruster.gameObject.SetActive(true);
        }

        protected override void Move()
        {
            transform.position -= Vector3.right * m_moveSpeed * Time.deltaTime;
        }

        public override void TakeDamage(float damageAmount)
        {
            base.TakeDamage(damageAmount);
            m_thruster.gameObject.SetActive(false);
        }
    }
}