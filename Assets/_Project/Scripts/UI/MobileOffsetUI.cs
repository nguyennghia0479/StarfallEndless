using UnityEngine;

public class MobileOffsetUI : MonoBehaviour
{
    [SerializeField] private float mobileYOffset = 40f;

    private void Start()
    {
#if UNITY_ANDROID
        if (transform.TryGetComponent<RectTransform>(out var rect))
        {
            Vector2 anchorPos = rect.anchoredPosition;
            anchorPos.y += mobileYOffset;
            rect.anchoredPosition = anchorPos;
        }
#endif
    }
}
