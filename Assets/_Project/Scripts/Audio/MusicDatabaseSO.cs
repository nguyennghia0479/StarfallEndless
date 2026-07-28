using UnityEngine;

[System.Serializable]
public struct MusicData
{
    public GameState musicState;
    public AudioClip[] clips;
}

[CreateAssetMenu(fileName = "Music Database", menuName = "Scriptable Objects/Database/Music Database")]
public class MusicDatabaseSO : ScriptableObject
{
    [SerializeField] private MusicData[] musicList;

    public AudioClip GetRandomMusic(GameState musicState)
    {
        foreach (var music in musicList)
        {
            if (music.musicState == musicState && music.clips.Length > 0)
            {
                int randomClip = Random.Range(0, music.clips.Length);
                return music.clips[randomClip];
            }
        }

        return null;
    }

   
}
