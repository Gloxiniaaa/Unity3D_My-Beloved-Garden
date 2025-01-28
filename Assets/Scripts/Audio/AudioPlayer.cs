using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    private AudioSource _audioSource;

    [Header("Listen on channel:")]
    [SerializeField] private AudioEventChannelSO _playSfxChannel;
    [SerializeField] private AudioEventChannelSO _uiSfxChannel;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _playSfxChannel.OnEventRaised += PlayAudio;
        _uiSfxChannel.OnEventRaised += PlayAudio;
    }

    private void PlayAudio(AudioGroupSO audioGroupSO)
    {
        _audioSource.PlayOneShot(audioGroupSO.GetRandomClip(), audioGroupSO.Volume);
    }

    private void OnDisable()
    {
        _playSfxChannel.OnEventRaised -= PlayAudio;
        _uiSfxChannel.OnEventRaised -= PlayAudio;
    }
}