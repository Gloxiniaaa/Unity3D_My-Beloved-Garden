using System;
using UnityEngine;

public abstract class UIButton : MonoBehaviour
{
    [HideInInspector] public static event Action OnClickButton;

    protected void PlayClickSfx()
    {
        OnClickButton?.Invoke();
    }
}