using System.Collections;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    [Header("Listen to")]
    [SerializeField] private GameObject _shovelCard;

    void OnEnable()
    {
        TurnOffShovel();
    }

    public void TurnOnShovel()
    {
        _shovelCard.SetActive(true);
        _shovelCard.transform.position = transform.position;
    }

    public void TurnOffShovel()
    {
        _shovelCard.SetActive(false);
    }

    public void UseShovel(System.Action callback = null)
    {
        _shovelCard.transform.position = transform.position;
        _shovelCard.transform.position += transform.forward;

        // Start a coroutine to wait and call the callback
        StartCoroutine(WaitAndCallback(0.5f, callback));
    }

    private IEnumerator WaitAndCallback(float delay, System.Action callback)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }
}