using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioGroupSO _clickSfx;
    [SerializeField] private AudioGroupSO _winSfx;
    [SerializeField] private AudioGroupSO _loseSfx;

    [Header("Listen on channel:")]
    [SerializeField] private AudioEventChannelSO _playSfxChannel;
    [SerializeField] private AudioEventChannelSO _uiSfxChannel;
    [SerializeField] private BoolEventChannelSO _onCompletionChannel;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _playSfxChannel.OnEventRaised += PlayAudio;
        _uiSfxChannel.OnEventRaised += PlayAudio;
        _onCompletionChannel.OnEventRaised += PlayEndGameAudio;
        UIButton.OnClickButton += PlayClickButtonAudio;
    }

    private void PlayClickButtonAudio()
    {
        _audioSource.PlayOneShot(_clickSfx.GetClip(), _clickSfx.Volume);
    }

    private void PlayEndGameAudio(bool win)
    {
        StartCoroutine(WaitAndPlayAudio(0.5f, win ? _winSfx : _loseSfx));
    }

    private IEnumerator WaitAndPlayAudio(float delay, AudioGroupSO audio)
    {
        yield return new WaitForSeconds(delay);
        _audioSource.PlayOneShot(audio.GetClip(), audio.Volume);

    }

    private void PlayAudio(AudioGroupSO audioGroupSO)
    {
        _audioSource.PlayOneShot(audioGroupSO.GetClip(), audioGroupSO.Volume);
    }

    private void OnDisable()
    {
        _playSfxChannel.OnEventRaised -= PlayAudio;
        _uiSfxChannel.OnEventRaised -= PlayAudio;
        _onCompletionChannel.OnEventRaised -= PlayEndGameAudio;
        UIButton.OnClickButton -= PlayClickButtonAudio;
    }
}