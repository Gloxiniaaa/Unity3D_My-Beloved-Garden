using UnityEngine;
using DG.Tweening;

public class TreeSway : MonoBehaviour
{
    [SerializeField][Range(0, 4)] private float swayAngle = 5f;  // Maximum sway angle (degrees)
    [SerializeField][Range(0, 2)] private float swayDuration = 1f;  // Time to complete one sway cycle

    private void Start()
    {
        StartSwaying();
    }

    private void StartSwaying()
    {
        transform.DOLocalRotate(new Vector3(0, 0, swayAngle), swayDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    void OnDisable()
    {
        transform.DOKill();
    }
}
