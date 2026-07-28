using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private MusicDatabaseSO musicDB;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private float duration = 2;

    private readonly float timeToTurnOff = 2;
    private Coroutine musicCoroutine;

    private void OnEnable()
    {
        UIEvents.OnMainMenuButtonClicked += PlayMainMenuTrack;
        GameEvents.OnGameReady += PlayMainGameTrack;
        UIEvents.OnEndGameButtonClicked += PlayGameOverTrack;
    }

    private void OnDisable()
    {
        UIEvents.OnMainMenuButtonClicked -= PlayMainMenuTrack;
        GameEvents.OnGameReady -= PlayMainGameTrack;
        UIEvents.OnEndGameButtonClicked -= PlayGameOverTrack;
    }

    private void Start()
    {
        PlayMainMenuTrack();
    }

    private void PlayMainMenuTrack()
    {
        StopPlayBGM();
        musicCoroutine = StartCoroutine(PlayRandomMusic(GameState.MainMenu));
    }

    private void PlayMainGameTrack()
    {
        StopPlayBGM();
        musicCoroutine = StartCoroutine(PlayRandomMusic(GameState.GamePlaying));
    }

    private void PlayGameOverTrack()
    {
        StopPlayBGM();
        musicCoroutine = StartCoroutine(PlayRandomMusic(GameState.GameOver));
    }

    private IEnumerator PlayRandomMusic(GameState musicState)
    {
        while (true)
        {
            if (musicSource.isPlaying)
                yield return AdjustVolume(musicSource, 0, duration);

            AudioClip musicClip = musicDB.GetRandomMusic(musicState);
            if (musicClip == null)
                yield break;

            musicSource.clip = musicClip;
            musicSource.volume = 0;
            musicSource.Play();
            StartCoroutine(AdjustVolume(musicSource, 1, duration));

            float musicLength = musicClip.length - (duration * timeToTurnOff);
            if (musicLength > 0)
                yield return new WaitForSecondsRealtime(musicLength);

            yield return AdjustVolume(musicSource, 0, duration);
            musicSource.Stop();
        }
    }

    private void StopPlayBGM()
    {
        if (musicCoroutine != null)
        {
            StopCoroutine(musicCoroutine);
            musicCoroutine = null;
        }
    }

    private IEnumerator AdjustVolume(AudioSource musicSource, float targetVolume, float duration)
    {
        float elapseTime = 0;
        float startVolume = musicSource.volume;

        while (elapseTime < duration)
        {
            musicSource.volume = Mathf.Lerp(startVolume, targetVolume, elapseTime / duration);
            elapseTime += Time.unscaledDeltaTime;
            yield return null;
        }

        musicSource.volume = targetVolume;
    }
}
