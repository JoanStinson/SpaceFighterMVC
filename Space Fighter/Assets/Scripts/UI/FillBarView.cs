using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class FillBarView : MonoBehaviour
    {
        [SerializeField] private Image m_whiteFillImage;
        [SerializeField] private Image m_fillImage;
        [SerializeField] private float m_lerpSpeed = 0.5f;

        private float m_currentValue;
        private float m_currentWhiteFillValue;
        private float m_maxValue;
        private float m_lerpTime;

        public void SetValue(float currentValue, float maxValue)
        {
            m_currentValue = currentValue;
            m_maxValue = maxValue;
            m_lerpTime = 0;
        }

        private void Update()
        {
            if (m_currentWhiteFillValue != m_currentValue)
            {
                m_currentWhiteFillValue = Mathf.Lerp(m_currentWhiteFillValue, m_currentValue, m_lerpTime);
                m_lerpTime += m_lerpSpeed * Time.deltaTime;
            }

            float whiteFillAmount = m_currentWhiteFillValue / m_maxValue;
            m_whiteFillImage.fillAmount = Mathf.Clamp(whiteFillAmount, 0, 1);

            float fillAmount = m_currentValue / m_maxValue;
            m_fillImage.fillAmount = Mathf.Clamp(fillAmount, 0, 1);
        }
    }
}