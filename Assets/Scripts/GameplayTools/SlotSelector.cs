using System;
using DG.Tweening;
using GameInput;
using UnityEngine;

public class SlotSelector : MonoBehaviour
{
    private GameObject _indicator;

    [Header("Listen to:")]
    [SerializeField] private InputReaderSO _inputReader;
    [SerializeField] private VoidEventChannelSO _ToggleSlotSelecionChannel;

    private void Awake()
    {
        _indicator = transform.GetChild(0).gameObject;
        _indicator.SetActive(false);
    }

    private void OnEnable()
    {
        _ToggleSlotSelecionChannel.OnEventRaised += ToggleSlotSelection;
    }

    [ContextMenu("Toggle Slot Selection")]
    private void ToggleSlotSelection()
    {
        if (_indicator.activeInHierarchy)
        {
            _inputReader.Move -= MoveIndicator;
            _indicator.SetActive(false);
            StopIndicatorAnimation();
        }
        else
        {
            _inputReader.Move += MoveIndicator;
            _indicator.SetActive(true);
            AnimateIndication();
            //TODO: set pos to player pos
        }
    }


    private void MoveIndicator(Vector2 input)
    {
        Vector3 direction = Helper.InputTo3dAxisDirection(input);
        if (Physics.Raycast(transform.position, direction, out RaycastHit hitInfo, 100f, 1 << Constant.LAND_LAYER))
        {
            transform.position = hitInfo.transform.position;
        }
    }

    private void AnimateIndication()
    {
        _indicator.transform.DOScale(0.75f, 0.5f).SetEase(Ease.InBack).SetLoops(-1, LoopType.Yoyo);
    }
    
    private void StopIndicatorAnimation()
    {
        _indicator.transform.DOKill();
        _indicator.transform.localScale = Vector3.one;
    }

    private void OnDisable()
    {
        _ToggleSlotSelecionChannel.OnEventRaised -= ToggleSlotSelection;
    }

}