using TMPro;
using UnityEngine;

public class SafeAreaUI : MonoBehaviour
{
    private RectTransform rectTransform;
    private Rect lastSafeArea = new Rect(0, 0, 0, 0);
    private Vector2 lastScreenSize = new Vector2(0, 0);

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Refresh();
    }

    void Update()
    {
        // Tự động cập nhật nếu xoay màn hình hoặc thay đổi độ phân giải
        if (lastSafeArea != Screen.safeArea || lastScreenSize.x != Screen.width || lastScreenSize.y != Screen.height)
        {
            Refresh();
        }
    }

    void Refresh()
    {
        Rect safeArea = Screen.safeArea;

        // 1. Tính toán Anchor cho phần trên và hai bên (lấy từ Safe Area chuẩn)
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        // Gán anchor cơ bản theo Safe Area
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;

        rectTransform.offsetMax = Vector2.zero; // Reset offset phía trên/phải về 0
        rectTransform.offsetMin = Vector2.zero;

        // 2. Xử lý riêng cho đáy (Bottom) để chống bị Navigation Bar che
        // Nếu safeArea.y == 0 (thường xảy ra trên Android), nghĩa là đáy dính sát mép và không có safe area vật lý ở đáy
        //if (safeArea.y <= 0)
        //{
        //    // Ép anchorMin.y về 0 để nó bắt đầu từ đáy màn hình, sau đó dùng offsetMin.y để đẩy lên
        //    rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x, 0);

        //    float bottomPadding = Screen.height * 0.08f;

        //    rectTransform.offsetMin = new Vector2(
        //        rectTransform.offsetMin.x,
        //        bottomPadding
        //    );
        //}
        //else
        //{
        //    // Nếu có safe area ở đáy thật (ví dụ iPhone có thanh Home Indicator)
        //rectTransform.offsetMin = Vector2.zero;
        //}
    }
}

