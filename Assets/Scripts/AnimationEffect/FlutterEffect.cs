using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FlutterEffect : MonoBehaviour
{
    IEnumerator Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            yield return new WaitForSeconds(0.1f);
            Flutter(transform.GetChild(i));
        }
    }

    private void Flutter(Transform t)
    {
        t.DOScaleX(0.6f,0.2f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Yoyo);
    }
}