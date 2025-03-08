using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[SelectionBase]
public class HardSoilSlot : MonoBehaviour
{
    // Means that player has to step on it twice to be able to plant flower on this block
    private int resistance = 2;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER_TAG))
        {
            resistance--;
            if (resistance == 0)
                gameObject.layer = Constant.LAND_LAYER;
        }
        else if (other.CompareTag(Constant.REVERSE_PLAYER_TAG))
        {
            resistance++;
            gameObject.layer = Constant.DEFAULT_LAYER;
        }
    }
}