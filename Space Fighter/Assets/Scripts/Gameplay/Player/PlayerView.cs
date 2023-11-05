using System.Collections;
using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public class PlayerView : MonoBehaviour, IDamageable
    {
        [Header("Stats")]
        [SerializeField] private float m_moveSpeed = 0.1f;
        [SerializeField] private float m_moveSmoothTime = 0.3f;
        [SerializeField] private float m_moveScreenPadding = 4f;
        [SerializeField] private float m_damagePower = 0f;

        [Header("Dependencies")]
        [SerializeField] private PlayerInput m_playerInput;
        [SerializeField] private PlayerWeapon m_playerWeapon;
        [SerializeField] private BulletLauncher m_bulletLauncher;
        [SerializeField] private Animator m_shipAnimator;
        [SerializeField] private Animator[] m_thrusters;
        [SerializeField] private BoxCollider2D m_boxCollider2D;
        [SerializeField] private Animator m_weaponMountPoint;

        [Inject]
        private ICoroutineService m_coroutineService;
        private GameView m_gameView;
        private GameModel m_gameModel;
        private Vector3 m_velocity = Vector3.zero;

        public void Initialize(GameView gameView, GameModel gameModel)
        {
            m_gameView = gameView;
            m_gameModel = gameModel;
            m_playerInput.onFireWeapon += FireWeapon;
            m_bulletLauncher.Initialize(gameModel);
        }

        private void FireWeapon()
        {
            m_playerWeapon.FireWeapon(m_bulletLauncher);
            m_weaponMountPoint.Play("MountPoint");
        }

        private void Update()
        {
            Vector3 targetPosition = transform.position + Vector3.up * m_playerInput.input.vertical * m_moveSpeed * Time.deltaTime;
            targetPosition.y = Mathf.Clamp(targetPosition.y, -m_moveScreenPadding, m_moveScreenPadding);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_velocity, m_moveSmoothTime);
        }

        public void TakeDamage(float damageAmount)
        {
            m_gameModel.currentHealth -= damageAmount;
            if (m_gameModel.currentHealth <= 0)
            {
                m_playerInput.enabled = false;
                m_shipAnimator.Play("ShipExplosion");
                foreach (var thruster in m_thrusters)
                {
                    thruster.gameObject.SetActive(false);
                }
                m_gameView.OnPlayerKilled();
            }
            else
            {
                m_coroutineService.StartExternalCoroutine(FlashPlayer());
            }
        }

        private IEnumerator FlashPlayer()
        {
            m_shipAnimator.Play("ShipFlash");
            foreach (var thruster in m_thrusters)
            {
                thruster.Play("Flash");
            }
            m_boxCollider2D.enabled = false;

            yield return new WaitForSeconds(2);

            m_shipAnimator.Play("ShipIdle");
            foreach (var thruster in m_thrusters)
            {
                thruster.Play("Thruster");
            }
            m_boxCollider2D.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(m_damagePower);
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
            m_shipAnimator.Play("ShipIdle");
            foreach (var thruster in m_thrusters)
            {
                thruster.gameObject.SetActive(true);
            }
            m_playerInput.enabled = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}