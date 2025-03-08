using UnityEngine;

public class SetupGameplayToolsUI : MonoBehaviour
{
    [SerializeField] private Transform _container;
    public void SpawnUI(GameObject uiPrefab){
        Instantiate(uiPrefab, _container);
    }
}