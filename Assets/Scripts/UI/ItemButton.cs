using UnityEngine;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO _UseItemChannel;
    
    public void OnClick()
    {
        _UseItemChannel.RaiseEvent();
    }
}