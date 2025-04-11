using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        
        public void RefreshMaxValue(float maxValue)
        {
            _slider.maxValue = maxValue;
        }

        public void ChangeValue(int value)
        {
            _slider.value = value;
        }
    }
}
