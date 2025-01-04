namespace Gimica.ProblemTwo
{
    using UnityEngine;

    public class UIEntity : MonoBehaviour
    {
        private void Awake()
        {
            if (!transform.TryGetComponent<RectTransform>(out var rectTransform))
            {
                return;
            }
            
            rectTransform.anchorMin = rectTransform.anchorMax = rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.position = Vector3.zero;
        }
    }
}