using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundDatabaseSO soundDB;
    [SerializeField] private AudioSource uiSource;
    [SerializeField] private GameObject soundEmitterPrefab;

    private void OnEnable()
    {
        GameEvents.OnShooted += PlayShootSFX;
        GameEvents.OnHit += PlayHitSFX;
        GameEvents.OnExploded += PlayExplodeSFX;
        GameEvents.OnHealed += PlayConsumeSFX;
        GameEvents.OnConsumed += PlayConsumeSFX;
        UIEvents.OnButtonClicked += PlayButtonSFX;
    }

    private void OnDisable()
    {
        GameEvents.OnShooted -= PlayShootSFX;
        GameEvents.OnHit -= PlayHitSFX;
        GameEvents.OnExploded -= PlayExplodeSFX;
        GameEvents.OnHealed -= PlayConsumeSFX;
        GameEvents.OnConsumed -= PlayConsumeSFX;
        UIEvents.OnButtonClicked -= PlayButtonSFX;
    }

    private void PlaySound(SoundType soundType, Vector2 position)
    {
        AudioClip audioClip = soundDB.GetRandomClip(soundType);
        if (audioClip != null)
        {
            if (position == Vector2.zero)
                uiSource.PlayOneShot(audioClip);
            else
            {
                GameObject emitterObject = ObjectPoolManager.Instance.GetPool(soundEmitterPrefab, position, Quaternion.identity);
                if (emitterObject.TryGetComponent<SoundEmitter>(out var soundEmitter))
                    soundEmitter.PlaySound(audioClip, position);
            }
        }
    }

    private void PlayShootSFX(Vector2 pos) => PlaySound(SoundType.Shoot, pos);
    private void PlayHitSFX(Vector2 pos) => PlaySound(SoundType.Hit, pos);
    private void PlayExplodeSFX(Vector2 pos) => PlaySound(SoundType.Explode, pos);
    private void PlayConsumeSFX(Vector2 pos) => PlaySound(SoundType.Consume, pos);
    private void PlayButtonSFX() => PlaySound(SoundType.Button, Vector2.zero);
}
