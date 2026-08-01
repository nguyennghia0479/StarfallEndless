using UnityEngine;

[CreateAssetMenu(fileName = "Background Database", menuName = "Scriptable Objects/Database/Background Database")]

public class BackgroundSDatabaseSO : ScriptableObject
{
    [SerializeField] private Sprite[] sprites;

    public Sprite[] Sprites => sprites;
}
