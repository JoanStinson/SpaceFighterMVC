using UnityEngine;

namespace JGM.Game
{
    public class PlayerView : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private float m_moveSpeed = 0.1f;
        [SerializeField] private float m_moveSmoothTime = 0.3f;
        [SerializeField] private float m_moveScreenPadding = 4f;

        [Header("Dependencies")]
        [SerializeField] private PlayerInput m_playerInput;
        [SerializeField] private PlayerWeapon m_playerWeapon;
        [SerializeField] private BulletLauncher m_bulletLauncher;

        private GameModel m_gameModel;
        private Vector3 m_velocity = Vector3.zero;

        public void Initialize(GameModel gameModel)
        {
            m_gameModel = gameModel;
            m_playerInput.onFireWeapon += FireWeapon;
        }

        private void FireWeapon()
        {
            m_playerWeapon.FireWeapon(m_bulletLauncher);
        }

        private void Update()
        {
            Vector3 targetPosition = transform.position + Vector3.up * m_playerInput.input.vertical * m_moveSpeed * Time.deltaTime;
            targetPosition.y = Mathf.Clamp(targetPosition.y, -m_moveScreenPadding, m_moveScreenPadding);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_velocity, m_moveSmoothTime);
        }
    }
}