using UnityEngine;

[CreateAssetMenu(fileName = "UndoToolSO", menuName = "Tool/UndoToolSO")]
public class UndoToolSO : GameplayToolSO
{
    public override void Setup(PlayerController playerController)
    {
        playerController.SetupUndoTool();
    }
}