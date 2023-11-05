using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class FillBarView : MonoBehaviour
    {
        [Header("White Fill")]
        [SerializeField] private Image m_whiteFillImage;
        [SerializeField] private float m_whiteFillLerpSpeed = 0.03f;

        [Header("Fill")]
        [SerializeField] private Image m_fillImage;
        [SerializeField] private float m_fillLerpSpeed = 1f;

        private float m_currentValue;
        private float m_currentWhiteFillValue;
        private float m_maxValue;

        private float m_whiteFillLerpTime;
        private float m_fillLerpTime;

        public void SetValue(float currentValue, float maxValue)
        {
            m_currentValue = currentValue;
            m_maxValue = maxValue;
            m_whiteFillLerpTime = 0;
            m_fillLerpTime = 0;
        }

        private void Update()
        {
            RefreshWhiteFill();
            RefreshFill();
        }

        private void RefreshWhiteFill()
        {
            if (m_currentWhiteFillValue != m_currentValue)
            {
                m_currentWhiteFillValue = Mathf.Lerp(m_currentWhiteFillValue, m_currentValue, m_whiteFillLerpTime);
                m_whiteFillLerpTime += m_whiteFillLerpSpeed * Time.deltaTime;
            }

            float whiteFillAmount = m_currentWhiteFillValue / m_maxValue;
            m_whiteFillImage.fillAmount = Mathf.Clamp(whiteFillAmount, 0, 1);
        }

        private void RefreshFill()
        {
            float fillAmount = m_currentValue / m_maxValue;
            fillAmount = Mathf.Clamp(fillAmount, 0, 1);

            if (m_fillImage.fillAmount != fillAmount)
            {
                m_fillImage.fillAmount = Mathf.Lerp(m_fillImage.fillAmount, fillAmount, m_fillLerpTime);
                m_fillLerpTime += m_fillLerpSpeed * Time.deltaTime;
            }
        }
    }
}