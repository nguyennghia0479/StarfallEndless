using UnityEngine;

public enum SoundType
{
    Shoot, Hit, Explode, Consume, Button
}

[System.Serializable]
public struct SoundData
{
    public SoundType type;
    public AudioClip[] clips;
}

[CreateAssetMenu(fileName = "Sound Database", menuName = "Scriptable Objects/Database/Sound Database")]
public class SoundDatabaseSO : ScriptableObject
{
    [SerializeField] private SoundData[] soundList;

    public AudioClip GetRandomClip(SoundType soundType)
    {
        foreach (var sound in soundList)
        {
            if (sound.type == soundType && sound.clips.Length > 0)
            {
                int randomClip = Random.Range(0, sound.clips.Length);
                return sound.clips[randomClip];
            }
        }

        return null;
    } 
}
