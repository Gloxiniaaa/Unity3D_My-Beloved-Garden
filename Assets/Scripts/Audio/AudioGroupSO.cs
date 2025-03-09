using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioGroupSO", menuName = "AudioGroupSO")]
public class AudioGroupSO : ScriptableObject
{
    [SerializeField] List<AudioClip> _audioClips;
    [SerializeField] private AudioType _audioType;

    [Range(0f, 1f)] public float Volume = 1;

    private int idx = -1;

    public AudioClip GetClip()
    {
        return _audioType switch
        {
            AudioType.SINGLE => _audioClips[0],
            AudioType.RAMDOM => _audioClips[Random.Range(0, _audioClips.Count)],
            AudioType.SEQUENTIAL => _audioClips[idx >= (_audioClips.Count - 1) ? idx = 0 : idx++],
            _ => null,
        };
    }
}

public enum AudioType
{
    SINGLE,
    RAMDOM,
    SEQUENTIAL,
    RANDOM_NO_REPEAT
}