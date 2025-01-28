using TMPro;
using UnityEngine;

public class FlowerCounter : MonoBehaviour
{
    private int _availableLandSlots;
    [SerializeField] private TextMeshProUGUI _flowerCounterText;
    [SerializeField] private IntEventChannelSO _countLandSlotsChannel;
    [SerializeField] private Vec3EventChannelSO _onLandSlotPlantedChannel;
    [SerializeField] private VoidEventChannelSO _onUndoFlowerBloom;
    private void OnEnable()
    {
        _countLandSlotsChannel.OnEventRaised += CountLandSlots;
        _onUndoFlowerBloom.OnEventRaised += AddAvailableLandSlot;
        _onLandSlotPlantedChannel.OnEventRaised += RemoveAvailableLandSlot;
    }

    private void AddAvailableLandSlot()
    {
        _availableLandSlots++;
        _flowerCounterText.text = _availableLandSlots.ToString();
    }

    private void RemoveAvailableLandSlot(Vector3 arg0)
    {
        _availableLandSlots--;
        _flowerCounterText.text = _availableLandSlots.ToString();
    }

    private void CountLandSlots(int value)
    {
        _availableLandSlots = value;
        _flowerCounterText.text =  _availableLandSlots.ToString();
    }

    private void OnDisable()
    {
        _countLandSlotsChannel.OnEventRaised -= CountLandSlots;
        _onUndoFlowerBloom.OnEventRaised -= AddAvailableLandSlot;
        _onLandSlotPlantedChannel.OnEventRaised -= RemoveAvailableLandSlot;
    }
}