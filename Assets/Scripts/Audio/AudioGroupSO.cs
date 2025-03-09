using UnityEngine;

[CreateAssetMenu(fileName = "AudioGroupSO", menuName = "AudioGroupSO")]
public class AudioGroupSO : ScriptableObject
{
    [SerializeField] AudioClip[] _audioClips;

    [Range(0f, 1f)] public float Volume = 1;

    public AudioClip GetRandomClip()
    {
        return _audioClips[Random.Range(0, _audioClips.Length)];
    }
    public AudioClip GetClip()
    {
        return _audioClips[0];
    }
}