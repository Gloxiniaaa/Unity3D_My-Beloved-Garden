using Unity.VisualScripting;
using UnityEngine;

public class CollapsingToggleButton : UIButton
{
    [SerializeField] private CollapseMode _collapseMode;
    private CollapsibleUI[] _collapsibleButtons;

    private void Awake()
    {
        int amount = _collapseMode == CollapseMode.FIRST_CHILD ? 1 : transform.childCount;
        _collapsibleButtons = new CollapsibleUI[amount];
        for (int i = 0; i < amount; i++)
        {
            CollapsibleUI btn = transform.GetChild(i).AddComponent<CollapsibleUI>();
            _collapsibleButtons[i] = btn;
        }
    }

    public void ToggleCollapsing()
    {
        PlayClickSfx();
        foreach (CollapsibleUI collapsibleButton in _collapsibleButtons)
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