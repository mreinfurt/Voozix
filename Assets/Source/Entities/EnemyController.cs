using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector2 Movement;

    public float Speed = 5f;

    private void Start()
    {
        GetComponent<ClampToScreenBounds>().OnHitScreenBounds += OnHitScreenBounds;
    }

    private void Spawn()
    {
        var screenBounds = Utility.GetScreenSpaceBounds();

        transform.position = new Vector3(Random.Range(screenBounds.x, screenBounds.x + screenBounds.width),
            Random.Range(screenBounds.y, screenBounds.y + screenBounds.height), 1);

        var horizontal = Random.Range(0, 10);

        if (horizontal <= 5)
        {
            Movement.x = 1;
        }
        else
        {
            Movement.y = 1;
        }
    }

    /// <summary>
    /// Reverse directions when hitting screen bounds
    /// </summary>
    /// <param name="direction"></param>
    private void OnHitScreenBounds(Vector2 direction)
    {
        if (direction.x >= 1)
        {
            Movement.x *= -1;
        }
        else
        {
            Movement.y *= -1;
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        var position = new Vector3(Speed * Movement.x * Time.deltaTime, Speed * Movement.y * Time.deltaTime);
        transform.position += position;
    }
}