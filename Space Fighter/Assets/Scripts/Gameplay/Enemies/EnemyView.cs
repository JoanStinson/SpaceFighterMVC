﻿using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public abstract class EnemyView : MonoBehaviour, IDamageable
    {
        public int scorePoints => m_scorePoints;

        [Header("Enemy Generic")]
        [SerializeField] protected Animator m_animator;
        [SerializeField] private BoxCollider2D m_boxCollider2D;
        [SerializeField] protected float m_moveSpeed = 0.1f;
        [SerializeField] protected float m_damagePower = 1f;
        [SerializeField] protected float m_health = 1f;
        [SerializeField] private float m_leftLimit = -9f;
        [SerializeField] private int m_scorePoints = 100;

        [Inject]
        private IAudioService m_audioService;
        protected Vector3 m_startPosition;
        protected bool m_startMovingUp;
        protected bool m_dead;

        private ComponentPool<EnemyView> m_pool;

        public virtual void Initialize(Vector3 startPosition, ComponentPool<EnemyView> pool, bool startMovingUp)
        {
            transform.position = startPosition;
            m_startPosition = startPosition;
            m_pool = pool;
            m_startMovingUp = startMovingUp;
            m_dead = false;
            m_animator.Play("Idle");
            m_boxCollider2D.enabled = true;
        }

        private void Update()
        {
            if (!m_dead)
            {
                Move();
            }

            if (transform.position.x < m_leftLimit)
            {
                Return();
            }
        }

        protected abstract void Move();

        // Used on Explosion Animation Event too
        public void Return()
        {
            if (m_pool == null)
            {
                Destroy(gameObject);
            }
            else
            {
                m_pool.Return(this);
            }
        }

        public virtual void TakeDamage(float damageAmount)
        {
            m_animator.Play("Explosion");
            m_audioService.Play(AudioFileNames.explosionSfx);
            m_boxCollider2D.enabled = false;
            m_dead = true;
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