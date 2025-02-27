using Unity.VisualScripting;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    private CollapsibleButton[] collapsibleButtons;

    private void Awake()
    {
        collapsibleButtons = new CollapsibleButton[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            CollapsibleButton btn = transform.GetChild(i).AddComponent<CollapsibleButton>();
            collapsibleButtons[i] = btn;
        }
    }

    public void ToggleOptions()
    {
        foreach (CollapsibleButton collapsibleButton in collapsibleButtons)
        {
            collapsibleButton.Toggle();
        }
    }
}