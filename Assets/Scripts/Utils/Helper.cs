using UnityEngine;

public class Helper
{
    public static Vector3 InputTo3dAxisDirection(Vector2 input)
    {
        Vector3 direction = Vector3.zero;
        if (input.x != 0)
        {
            direction = new Vector3(input.x, 0, 0);
        }
        else if (input.y != 0)
        {
            direction = new Vector3(0, 0, input.y);
        }
        return direction.normalized;
    }
}