using System.Collections;
using UnityEngine;

public class SoundEmitter : PooledObject
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float pitchRange = .1f;

    public void PlaySound(AudioClip clip, Vector2 position)
    {
        audioSource.clip = clip;
        audioSource.pitch = Random.Range(1 - pitchRange, 1 + pitchRange);
        audioSource.transform.position = position;
        audioSource.Play();

        StartCoroutine(ReturnToPoolRoutine(clip.length));
    }

    private IEnumerator ReturnToPoolRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        Pool.Release(gameObject);
    }
}
