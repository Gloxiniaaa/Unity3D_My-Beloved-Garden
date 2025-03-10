using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioGroupSO _bgm;
    [Header("Listen to:")]
    [SerializeField] private BoolEventChannelSO _onCompletionChannel;
    [SerializeField] private BoolEventChannelSO _LoadLevelChannel;

    [SerializeField] private VoidEventChannelSO _toggleBGMChannel;
    private bool _mute = false;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
    }

    private void OnEnable()
    {
        _onCompletionChannel.OnEventRaised += TurnOffBGM;
        _toggleBGMChannel.OnEventRaised += ToggleBGM;
        _LoadLevelChannel.OnEventRaised += PlayBgmOnNewLevel;
        PlayBgmOnNewLevel();
    }

    private void TurnOffBGM(bool arg0)
    {
        _audioSource.Stop();
    }

    private void PlayBgmOnNewLevel(bool isNextLevel = true)
    {
        if (_mute)
            return;
        _audioSource.volume = _bgm.Volume;
        _audioSource.clip = _bgm.GetClip();
        _audioSource.Play();
    }

    private void ToggleBGM()
    {
        if (_mute)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Pause();
        }
        _mute = !_mute;
    }

    void OnDisable()
    {
        _onCompletionChannel.OnEventRaised -= TurnOffBGM;
        _toggleBGMChannel.OnEventRaised -= ToggleBGM;
        _LoadLevelChannel.OnEventRaised -= PlayBgmOnNewLevel;
    }
}