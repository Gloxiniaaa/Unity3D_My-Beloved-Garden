using UnityEngine;

public class SetupGameplayToolsUI : MonoBehaviour
{
    [SerializeField] private Transform _container;
    public void SpawnUI(GameObject uiPrefab)
    {
        Instantiate(uiPrefab, _container);
    }

    public void DestroyToolUIs()
    {
        for (int i = 0; i < _container.childCount; i++)
        {
            Destroy(_container.GetChild(i).gameObject);
        }
    }
}