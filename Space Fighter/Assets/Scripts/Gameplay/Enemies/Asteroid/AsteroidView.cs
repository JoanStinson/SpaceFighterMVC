using System;
using UnityEngine;

namespace JGM.Game
{
    public class AsteroidView : EnemyView
    {
        public event Action OnAsteroidBreak = delegate { };

        [Header("Asteroid Specifics")]
        [SerializeField] private float m_rotationSpeed = 30f;
        [SerializeField] private float m_frequency = 0.5f;
        [SerializeField] private float m_magnitude = 4f;

        protected override void Move()
        {
            transform.rotation *= Quaternion.Euler(0f, 0f, m_rotationSpeed * Time.deltaTime);
            m_startPosition -= Vector3.right * Time.deltaTime * m_moveSpeed;
            float sign = m_startMovingUp ? 1 : -1;
            transform.position = m_startPosition + (Vector3.up * sign) * Mathf.Sin(m_frequency * Time.time) * m_magnitude;
        }

        public override void TakeDamage(float damageAmount)
        {
            base.TakeDamage(damageAmount);
            OnAsteroidBreak?.Invoke();
        }
    }
}