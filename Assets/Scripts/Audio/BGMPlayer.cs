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

    private void ChangeVolume(int arg0)
    {
        _bgm.Volume = arg0;
        _audioSource.volume = _bgm.Volume;
        if (arg0 == 0)
        {
            _audioSource.Stop();
        }
    }

    void Start()
    {
        _audioSource.clip = _bgm.GetClip();
        _audioSource.volume = _bgm.Volume;
        _audioSource.loop = true;
        _audioSource.PlayDelayed(1f);
    }

    void OnDisable()
    {
        _bgmVolumeChannel.OnEventRaised -= ChangeVolume;
        _onCompletionChannel.OnEventRaised -= TurnOffBGM;
    }



}