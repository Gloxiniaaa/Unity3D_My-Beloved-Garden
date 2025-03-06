using TMPro;
using UnityEngine;

public class FlowerCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _flowerCounterText;
    [Header("Listen to:")]
    [SerializeField] private IntEventChannelSO _bindLandslotToUI;

    void OnEnable()
    {
        _bindLandslotToUI.OnEventRaised += Updatext;
    }

    private void Updatext(int availableLandSlots)
    {
        _flowerCounterText.text = availableLandSlots.ToString();
    }

    void OnDisable()
    {
        _bindLandslotToUI.OnEventRaised -= Updatext;
    }
}