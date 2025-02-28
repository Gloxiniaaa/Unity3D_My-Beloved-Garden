using UnityEngine;

public class RandomCurvlyWander : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionTime = 2f;
    public float curveIntensity = 2f;

    private Vector2 moveDirection;
    private float timer;
    private float curveAngle = 0f;

    void Start()
    {
        ChangeDirection();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ChangeDirection();
        }

        curveAngle += curveIntensity * Time.deltaTime * Random.Range(0.5f, 1.5f);
        Vector2 curvedDirection = new Vector2(
            moveDirection.x * Mathf.Cos(curveAngle) - moveDirection.y * Mathf.Sin(curveAngle),
            moveDirection.x * Mathf.Sin(curveAngle) + moveDirection.y * Mathf.Cos(curveAngle)
        ).normalized;

        float randomSpeed = moveSpeed * Random.Range(0.8f, 1.2f);
        transform.Translate(randomSpeed * Time.deltaTime * curvedDirection);
    }

    void ChangeDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
        timer = changeDirectionTime * Random.Range(0.8f, 1.2f);
        curveAngle = 0f;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, moveDirection);
    }
} 
