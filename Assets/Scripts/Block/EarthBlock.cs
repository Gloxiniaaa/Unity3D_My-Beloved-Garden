using UnityEngine;

public class EarthBlock : MonoBehaviour
{
    [SerializeField] private float _groundOffset;

    [Header("Broadcast on:")]
    [SerializeField] private Vec3EventChannelSO _spawnFlowerChannel;


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER_TAG))
        {
            _spawnFlowerChannel.RaiseEvent(transform.position + Vector3.up * _groundOffset);
        }
    }
}