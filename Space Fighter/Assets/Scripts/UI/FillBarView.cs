using UnityEngine;
using UnityEngine.UI;

namespace JGM.Game
{
    public class FillBarView : MonoBehaviour
    {
        [SerializeField] 
        private Image m_fillImage;

        public void SetValue(float currentValue, float maxValue)
        {
            float fillAmount = currentValue / maxValue;
            Mathf.Clamp(fillAmount, 0, 1);
            m_fillImage.fillAmount = fillAmount;
        }
    }
}