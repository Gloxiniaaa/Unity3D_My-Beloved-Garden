using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FlowerCounterSO", menuName = "FlowerCounterSO", order = 0)]
public class FlowerCounterSO : ScriptableObject
{
    public int TotalLandSlots { get; private set; }
    public int AvailableLandSlots { get; private set; }

    [Header("Listen to:")]
    [SerializeField] private IntEventChannelSO _countLandSlotsChannel;
    [SerializeField] private Vec3EventChannelSO _onLandSlotPlantedChannel;
    [SerializeField] private VoidEventChannelSO _onUndoFlowerBloom;
    [SerializeField] private VoidEventChannelSO _onReachCheckPoint;


    [Header("Broadcast on:")]
    [SerializeField] private IntEventChannelSO _bindLandslotToUI;
    [SerializeField] private BoolEventChannelSO _onCompletionChannel;

    private void OnEnable()
    {
        AvailableLandSlots = 0;
        TotalLandSlots = 0;
        _countLandSlotsChannel.OnEventRaised += CountLandSlots;
        _onUndoFlowerBloom.OnEventRaised += AddAvailableLandSlot;
        _onLandSlotPlantedChannel.OnEventRaised += RemoveAvailableLandSlot;
        _onReachCheckPoint.OnEventRaised += CheckForCompletion;
    }

    private void CheckForCompletion()
    {
        Debug.Log(AvailableLandSlots);
        if (AvailableLandSlots == 0)
        {
            _onCompletionChannel.RaiseEvent(true);
            Debug.Log("You have completed the level");
        }
        else
        {
            _onCompletionChannel.RaiseEvent(false);
            Debug.Log("Missing " + (AvailableLandSlots) + " flowers");
        }
    }

    private void AddAvailableLandSlot()
    {
        AvailableLandSlots++;
        _bindLandslotToUI.RaiseEvent(AvailableLandSlots);
    }

    private void RemoveAvailableLandSlot(Vector3 arg0)
    {
        AvailableLandSlots--;
        _bindLandslotToUI.RaiseEvent(AvailableLandSlots);
    }

    public void CountLandSlots(int value)
    {
        TotalLandSlots = value;
        AvailableLandSlots = value;
        _bindLandslotToUI.RaiseEvent(value);
        Debug.Log("Hey there are a total of " + value + " land slots you must plant flowers on");
    }

    private void OnDisable()
    {
        _countLandSlotsChannel.OnEventRaised -= CountLandSlots;
        _onUndoFlowerBloom.OnEventRaised -= AddAvailableLandSlot;
        _onLandSlotPlantedChannel.OnEventRaised -= RemoveAvailableLandSlot;
        _onReachCheckPoint.OnEventRaised -= CheckForCompletion;
    }
}