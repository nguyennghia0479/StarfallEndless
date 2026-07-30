using UnityEngine;

public class ParticlePoolable : PooledObject
{
    [SerializeField] private ParticleSystem particleSystem;

    private void OnEnable()
    {
        PlayVFX();
    }

    private void OnParticleSystemStopped()
    {
        Pool.Release(gameObject);
    }

    private void PlayVFX()
    {
        if (particleSystem != null)
        {
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particleSystem.Play(true);
        }
    }
}
