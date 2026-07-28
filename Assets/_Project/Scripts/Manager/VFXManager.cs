using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitVFX;
    [SerializeField] private ParticleSystem explodeVFX;
    [SerializeField] private ParticleSystem healVFX;
    [SerializeField] private ParticleSystem consumedVFX;

    private void OnEnable()
    {
        GameEvents.OnHit += PlayHitVFX;
        GameEvents.OnExploded += PlayExplosionVFX;
        GameEvents.OnHealed += PlayHealVFX;
        GameEvents.OnConsumed += PlayConsumeVFX;
    } 

    private void OnDisable()
    {
        GameEvents.OnHit -= PlayHitVFX;
        GameEvents.OnExploded -= PlayExplosionVFX;
        GameEvents.OnHealed -= PlayHealVFX;
        GameEvents.OnConsumed -= PlayConsumeVFX;
    }

    private void PlayHitVFX(Vector2 position)
    {
        Instantiate(hitVFX, position, Quaternion.identity);
    }

    private void PlayExplosionVFX(Vector2 position)
    {
        Instantiate(explodeVFX, position, Quaternion.identity);
    }

    private void PlayHealVFX(Vector2 position)
    {
        Instantiate(healVFX, position, Quaternion.identity);
    }

    private void PlayConsumeVFX(Vector2 position)
    {
        Instantiate(consumedVFX, position, Quaternion.identity);
    }
}
