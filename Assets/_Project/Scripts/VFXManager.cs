using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject explodeVFX;

    private void OnEnable()
    {
        GameEvents.OnExploded += PlayExplosionVFX;
    }

    private void OnDisable()
    {
        GameEvents.OnExploded -= PlayExplosionVFX;
    }

    private void PlayExplosionVFX(Vector2 position)
    {
        Instantiate(explodeVFX, position, Quaternion.identity);
    }
}
