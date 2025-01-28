using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CompletionChecker : MonoBehaviour
{
    private int _landSlots;
    private int _plantedLandSlots = 0;

    [Header("Broadcast on:")]
    [SerializeField] private BoolEventChannelSO _onCompletionChannel;

    [Header("Listen to:")]
    [SerializeField] private IntEventChannelSO _countLandSlotsChannel;
    [SerializeField] private Vec3EventChannelSO _onLandSlotPlantedChannel;


    private void OnEnable()
    {
        _countLandSlotsChannel.OnEventRaised += CountLandSlots;
        _onLandSlotPlantedChannel.OnEventRaised += AddPlantedLandSlot;
    }

    private void AddPlantedLandSlot(Vector3 arg0)
    {
        _plantedLandSlots++;
    }

    private void CountLandSlots(int amount)
    {
        _landSlots = amount;
        Debug.Log("Hey there are a total of " + _landSlots + " land slots you must plant flowers on");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER_TAG))
        {
            if (_plantedLandSlots >= _landSlots)
            {
                _onCompletionChannel.RaiseEvent(true);
                Debug.Log("You have completed the level");
            }
            else
            {
                _onCompletionChannel.RaiseEvent(false);
                Debug.Log("Missing " + (_landSlots - _plantedLandSlots) + " flowers");
            }
        }
    }

    private void OnDisable()
    {
        _countLandSlotsChannel.OnEventRaised -= CountLandSlots;
        _onLandSlotPlantedChannel.OnEventRaised -= AddPlantedLandSlot;
    }
}