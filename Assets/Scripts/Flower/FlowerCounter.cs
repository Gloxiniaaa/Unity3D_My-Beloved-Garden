using UnityEngine;

public class FlowerCounter : MonoBehaviour
{
    private int _totalLandSlots;
    private int _availableLandSlots;

    [Header("Listen to:")]
    [SerializeField] private IntEventChannelSO _countLandSlotsChannel;
    [SerializeField] private Vec3EventChannelSO _onLandSlotPlantedChannel;
    [SerializeField] private VoidEventChannelSO _onUndoFlowerBloom;
    [SerializeField] private VoidEventChannelSO _onReachCheckPoint;
    [SerializeField] private VoidEventChannelSO _onStepOnFlower;


    [Header("Broadcast on:")]
    [SerializeField] private IntEventChannelSO _bindLandslotToUI;
    [SerializeField] private BoolEventChannelSO _onCompletionChannel;

    private void OnEnable()
    {
        _availableLandSlots = 0;
        _totalLandSlots = 0;
        _countLandSlotsChannel.OnEventRaised += CountLandSlots;
        _onUndoFlowerBloom.OnEventRaised += AddAvailableLandSlot;
        _onLandSlotPlantedChannel.OnEventRaised += RemoveAvailableLandSlot;
        _onReachCheckPoint.OnEventRaised += CheckForCompletion;
        _onStepOnFlower.OnEventRaised += OnStepOnFlower;
    }

    private void OnStepOnFlower()
    {
        _onCompletionChannel.RaiseEvent(false);
    }

    private void CheckForCompletion()
    {
        Debug.Log(_availableLandSlots);
        _onCompletionChannel.RaiseEvent(_availableLandSlots == 0);
    }

    private void AddAvailableLandSlot()
    {
        _availableLandSlots++;
        _bindLandslotToUI.RaiseEvent(_availableLandSlots);
    }

    private void RemoveAvailableLandSlot(Vector3 arg0)
    {
        _availableLandSlots--;
        _bindLandslotToUI.RaiseEvent(_availableLandSlots);
    }

    public void CountLandSlots(int value)
    {
        _totalLandSlots = value;
        _availableLandSlots = value;
        _bindLandslotToUI.RaiseEvent(value);
        Debug.Log("Hey there are a total of " + value + " land slots you must plant flowers on");
    }

    private void OnDisable()
    {
        _countLandSlotsChannel.OnEventRaised -= CountLandSlots;
        _onUndoFlowerBloom.OnEventRaised -= AddAvailableLandSlot;
        _onLandSlotPlantedChannel.OnEventRaised -= RemoveAvailableLandSlot;
        _onReachCheckPoint.OnEventRaised -= CheckForCompletion;
        _onStepOnFlower.OnEventRaised -= OnStepOnFlower;
    }
}