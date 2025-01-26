using UnityEngine;

public class EarthBlock : MonoBehaviour
{
    private bool _isPlanted = false;
    [SerializeField] private float _groundOffset;

    [Header("Broadcast on:")]
    [SerializeField] private Vec3EventChannelSO _spawnFlowerChannel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER_TAG))
        {
            if (_isPlanted)
            {
                Debug.Log("You stepped on a planted block");
            }
            else
            {
                _isPlanted = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER_TAG))
        {
            _spawnFlowerChannel.RaiseEvent(transform.position + Vector3.up * _groundOffset);
        }
    }
}