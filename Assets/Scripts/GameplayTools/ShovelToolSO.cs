using UnityEngine;

[CreateAssetMenu(fileName = "ShovelToolSO", menuName = "Tool/ShovelToolSO")]
public class ShovelToolSO : GameplayToolSO
{
    public override void Setup(PlayerController playerController)
    {
        playerController.SetupShovelTool();
    }
}