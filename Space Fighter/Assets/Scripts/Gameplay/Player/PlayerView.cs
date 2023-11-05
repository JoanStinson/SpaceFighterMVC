using System.Collections;
using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public class PlayerView : MonoBehaviour, IDamageable
    {
        [Header("General")]
        [SerializeField] private PlayerInput m_playerInput;
        [SerializeField] private BoxCollider2D m_boxCollider2D;
        [SerializeField] private SpriteRenderer m_shipSprite;
        [SerializeField] private Animator m_shipAnimator;
        [SerializeField] private Animator[] m_thrusters;

        [Header("Stats")]
        [SerializeField] private float m_moveSpeed = 0.1f;
        [SerializeField] private float m_moveSmoothTime = 0.3f;
        [SerializeField] private float m_moveScreenPadding = 4f;
        [SerializeField] private float m_damagePower = 0f;

        [Header("Weapon")]
        [SerializeField] private PlayerWeapon m_playerWeapon;
        [SerializeField] private BulletLauncher m_bulletLauncher;
        [SerializeField] private MissileLauncher m_missileLauncher;
        [SerializeField] private Animator m_weaponMountPoint;

        [Inject] private ICoroutineService m_coroutineService;
        [Inject] private IAudioService m_audioService;

        private GameView m_gameView;
        private GameModel m_gameModel;
        private Vector3 m_velocity = Vector3.zero;

        public int scorePoints => 0;

        public void Initialize(GameView gameView, GameModel gameModel)
        {
            m_gameView = gameView;
            m_gameModel = gameModel;
            m_playerInput.onFireWeapon += FireWeapon;
            m_bulletLauncher.Initialize(gameModel);
            m_missileLauncher.Initialize(gameModel);
        }

        private void FireWeapon()
        {
            var launcher = m_playerInput.bulletsSelected ? m_bulletLauncher as ILauncher : m_missileLauncher;
            m_playerWeapon.FireWeapon(launcher);
            m_weaponMountPoint.Play("MountPoint");
            string audioSfx = m_playerInput.bulletsSelected ? AudioFileNames.bulletSfx : AudioFileNames.missileSfx;
            m_audioService.Play(audioSfx);
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
                m_boxCollider2D.enabled = false;
                m_playerInput.enabled = false;
                m_shipAnimator.Play("ShipExplosion");
                foreach (var thruster in m_thrusters)
                {
                    thruster.gameObject.SetActive(false);
                }
                StopAllCoroutines();
                m_gameView.OnPlayerKilled();
                m_audioService.Play(AudioFileNames.playerDieSfx);
            }
            else
            {
                m_coroutineService.StartExternalCoroutine(FlashPlayer());
                m_audioService.Play(AudioFileNames.playerHitSfx);
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
            ReturnShipToRegularState();
        }

        private void ReturnShipToRegularState()
        {
            m_shipAnimator.Play("ShipIdle");
            foreach (var thruster in m_thrusters)
            {
                thruster.Play("Thruster");
                thruster.gameObject.SetActive(true);
            }
            m_boxCollider2D.enabled = true;
            m_playerInput.enabled = true;
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
            ReturnShipToRegularState();
            m_shipSprite.enabled = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        // Used on Explosion Animation Event
        public void HidePlayer()
        {
            m_shipSprite.enabled = false;
        }
    }
}