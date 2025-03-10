using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioGroupSO _bgm;
    [Header("Listen to:")]
    [SerializeField] private IntEventChannelSO _bgmVolumeChannel;
    [SerializeField] private BoolEventChannelSO _onCompletionChannel;


    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
    }

    private void OnEnable()
    {
        _bgmVolumeChannel.OnEventRaised += ChangeVolume;
        _onCompletionChannel.OnEventRaised += TurnOffBGM;
    }

    private void TurnOffBGM(bool arg0)
    {
        _audioSource.Stop();
    }

    public void TurnOnBGM()
    {
        if (_bgm.Volume == 0)
            return;
        _audioSource.volume = _bgm.Volume;
        _audioSource.clip = _bgm.GetClip();
        _audioSource.Play();
    }

    private void ChangeVolume(int arg0)
    {
        _bgm.Volume = arg0;
        _audioSource.volume = _bgm.Volume;
        if (arg0 == 0)
        {
            _audioSource.Stop();
        }
    }


    void OnDisable()
    {
        _bgmVolumeChannel.OnEventRaised -= ChangeVolume;
        _onCompletionChannel.OnEventRaised -= TurnOffBGM;
    }



}