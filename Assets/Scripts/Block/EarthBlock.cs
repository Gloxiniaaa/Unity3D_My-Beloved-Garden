using UnityEngine;

public class EarthBlock : MonoBehaviour
{
    private bool _isPlanted = false;

    [Header("Broadcast on:")]
    [SerializeField] private Vec3EventChannelSO _spawnFlowerChannel;


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Constant.PLAYER_TAG))
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

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag(Constant.PLAYER_TAG))
        {
            _spawnFlowerChannel.RaiseEvent(transform.position + Vector3.up * 0.5f); // with a 0.5 offset to the ground
        }
    }
    
}