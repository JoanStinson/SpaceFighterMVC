using UnityEngine;

namespace JGM.Game
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed = 0.1f;
        [SerializeField] private float m_screenBoundaryPadding = 4f;

        private void Update()
        {
            var newPosition = transform.position + Vector3.up * Input.GetAxis("Vertical") * m_moveSpeed * Time.deltaTime;
            newPosition.y = Mathf.Clamp(newPosition.y, -m_screenBoundaryPadding, m_screenBoundaryPadding);
            transform.position = newPosition;
        }
    }
}