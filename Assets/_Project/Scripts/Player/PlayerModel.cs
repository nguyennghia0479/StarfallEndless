using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private SpriteRenderer shipSprite;
    [SerializeField] private SpriteRenderer shieldSprite;
    [SerializeField] private SpriteRenderer[] shipVisuals;
    [SerializeField] private Transform[] gunPoints;

    public SpriteRenderer ShipSprite => shipSprite;
    public SpriteRenderer ShieldSprite => shieldSprite;
    public SpriteRenderer[] ShipVisuals => shipVisuals;
    public Transform[] GunPoints => gunPoints;
}
