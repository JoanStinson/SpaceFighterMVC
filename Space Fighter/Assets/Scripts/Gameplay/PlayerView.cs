using UnityEngine;

namespace JGM.Game
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed = 0.1f;
        [SerializeField] private float m_smoothTime = 0.3f;
        [SerializeField] private float m_moveScreenPadding = 4f;

        private Vector3 m_velocity = Vector3.zero;

        private void Update()
        {
            Vector3 targetPosition = transform.position + Vector3.up * Input.GetAxis("Vertical") * m_moveSpeed * Time.deltaTime;
            targetPosition.y = Mathf.Clamp(targetPosition.y, -m_moveScreenPadding, m_moveScreenPadding);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_velocity, m_smoothTime);
            transform.position = targetPosition;
        }
    }
}