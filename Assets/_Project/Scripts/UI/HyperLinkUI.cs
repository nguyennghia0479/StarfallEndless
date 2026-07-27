using UnityEngine;

public class HyperLinkUI : MonoBehaviour
{
    [SerializeField] private string url;

    // Use for button on click in inspector
    public void OpenUrl() => Application.OpenURL(url);
}
