using Unity.VisualScripting;
using UnityEngine;

public class CollapsingToggleButton : MonoBehaviour
{
    [SerializeField] private CollapseMode _collapseMode;
    private CollapsibleButton[] _collapsibleButtons;

    private void Awake()
    {
        int amount = _collapseMode == CollapseMode.FIRST_CHILD ? 1 : transform.childCount;
        _collapsibleButtons = new CollapsibleButton[amount];
        for (int i = 0; i < amount; i++)
        {
            CollapsibleButton btn = transform.GetChild(i).AddComponent<CollapsibleButton>();
            _collapsibleButtons[i] = btn;
        }
    }

    public void ToggleCollapsing()
    {
        foreach (CollapsibleButton collapsibleButton in _collapsibleButtons)
        {
            collapsibleButton.Toggle();
        }
    }
}

public enum CollapseMode
{
    FIRST_CHILD,
    ALL_CHILDREN
}