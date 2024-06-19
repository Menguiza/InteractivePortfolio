using TMPro;
using UnityEngine;

namespace UI
{
#if UNITY_EDITOR || DEBUG
    public class FPSDisplay : MonoBehaviour
    {
        public TMP_Text display_Text;
    
        private int avgFrameRate;
 
        public void Update ()
        {
            float current = 0;
            current = (int)(1f / Time.unscaledDeltaTime);
            avgFrameRate = (int)current;
            display_Text.text = avgFrameRate.ToString() + " FPS";
        }
    }
#endif
}