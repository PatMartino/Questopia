using UnityEngine;

namespace UI
{
    public class JoystickDisabler : MonoBehaviour
    {
        [SerializeField] private GameObject joystick;
        [SerializeField] private float newPosY;
        [SerializeField] private float newHeight;
        
        private Vector2 originalAnchoredPosition;
        private Vector2 originalSizeDelta;

        void OnEnable()
        {
            RectTransform rectTransform = joystick.GetComponent<RectTransform>();
            
            originalAnchoredPosition = rectTransform.anchoredPosition;
            originalSizeDelta = rectTransform.sizeDelta;

            Vector2 anchoredPosition = rectTransform.anchoredPosition;
            anchoredPosition.y = newPosY;
            rectTransform.anchoredPosition = anchoredPosition;
            
            Vector2 sizeDelta = rectTransform.sizeDelta;
            sizeDelta.y = newHeight;
            rectTransform.sizeDelta = sizeDelta;
        }

        void OnDisable()
        {
            RectTransform rectTransform = joystick.GetComponent<RectTransform>();
            
            rectTransform.anchoredPosition = originalAnchoredPosition;
            rectTransform.sizeDelta = originalSizeDelta;
        }
    }
}
