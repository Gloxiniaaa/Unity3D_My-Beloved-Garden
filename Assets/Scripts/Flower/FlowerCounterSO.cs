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


    [Header("Broadcast on:")]
    [SerializeField] private IntEventChannelSO _bindLandslotToUI;

    private void OnEnable()
    {
        _countLandSlotsChannel.OnEventRaised += CountLandSlots;
        _onUndoFlowerBloom.OnEventRaised += AddAvailableLandSlot;
        _onLandSlotPlantedChannel.OnEventRaised += RemoveAvailableLandSlot;
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
    }
}