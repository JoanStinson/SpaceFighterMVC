using UnityEngine;

namespace JGM.Game
{
    public class ParallaxBackground : MonoBehaviour
    {
        [SerializeField] private float m_scrollSpeed = -100f;
        [SerializeField] private float m_rightEdge = 41;
        [SerializeField] private float m_leftEdge = -8;

        private void Update()
        {
            float localPositionX = transform.localPosition.x + m_scrollSpeed * Time.deltaTime;
            transform.localPosition = new Vector3(localPositionX, transform.localPosition.y, transform.localPosition.z);

            if (transform.localPosition.x <= m_leftEdge)
            {
                transform.localPosition = new Vector3(m_rightEdge, transform.localPosition.y, transform.localPosition.z);
            }
        }
    }
}