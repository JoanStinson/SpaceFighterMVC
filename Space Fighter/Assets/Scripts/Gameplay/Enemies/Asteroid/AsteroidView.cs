using System;
using UnityEngine;

namespace JGM.Game
{
    public class AsteroidView : MonoBehaviour, IDamageable
    {
        public event Action OnAsteroidBreak = delegate { };

        [SerializeField] private float m_moveSpeed = 0.1f;
        [SerializeField] private float m_rotationSpeed = 30f;
        [SerializeField] private float m_frequency = 0.5f;
        [SerializeField] private float m_magnitude = 4f;
        [SerializeField] private float m_damagePower = 1f;
        [SerializeField] private float m_health = 1f;
        [SerializeField] private Animator m_animator;

        private ComponentPool<AsteroidView> m_pool;
        private Vector3 m_newPosition;
        private bool m_startMovingUp;
        private bool m_dead;

        public void Initialize(bool startMovingUp, Vector3 startPosition, ComponentPool<AsteroidView> pool)
        {
            m_startMovingUp = startMovingUp;
            transform.position = startPosition;
            m_newPosition = startPosition;
            m_pool = pool;
            m_animator.Play("Idle");
            m_dead = false;
        }

        private void Update()
        {
            if (m_dead)
            {
                return;
            }

            transform.rotation *= Quaternion.Euler(0f, 0f, m_rotationSpeed * Time.deltaTime);
            m_newPosition -= Vector3.right * Time.deltaTime * m_moveSpeed;
            float sign = m_startMovingUp ? 1 : -1;
            transform.position = m_newPosition + (Vector3.up * sign) * Mathf.Sin(m_frequency * Time.time) * m_magnitude;
        }

        public void TakeDamage(float damageAmount)
        {
            OnAsteroidBreak?.Invoke();
            m_animator.Play("Explosion");
            m_dead = true;
        }

        // Used on Explosion Animation Event
        public void ReturnAsteroid()
        {
            m_pool?.Return(this);
            if (m_pool == null)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(m_damagePower);
            }
        }
    }
}