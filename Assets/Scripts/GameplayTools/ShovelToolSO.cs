using UnityEngine;

[CreateAssetMenu(fileName = "ShovelToolSO", menuName = "Tool/ShovelToolSO")]
public class ShovelToolSO : GameplayToolSO
{
    [SerializeField] private Shovel _shovelPrefab ;
    public override void Setup(PlayerController playerController)
    {
        playerController.SetupShovelTool(_shovelPrefab);
    }
}