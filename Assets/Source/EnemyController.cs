using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector2 Movement;

    public float Speed = 5f;

    private void Start()
    {
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

    private void Update()
    {
        HandleWallBounds();
        HandleMovement();
    }

    private void HandleMovement()
    {
        Speed = 5f;
        var position = new Vector3(Speed * Movement.x * Time.deltaTime, Speed * Movement.y * Time.deltaTime);

        this.transform.position += position;
    }

    private void HandleWallBounds()
    {
        var newPosition = transform.position;
        var mainCamera = Camera.main;
        var cameraPosition = mainCamera.transform.position;

        var screenSpaceOffset = 0.2f;
        var distance = new Vector2(mainCamera.aspect * mainCamera.orthographicSize - screenSpaceOffset,
            mainCamera.orthographicSize - screenSpaceOffset);

        var xMax = cameraPosition.x + distance.x;
        var xMin = cameraPosition.x - distance.x;

        var yMax = cameraPosition.y + distance.y;
        var yMin = cameraPosition.y - distance.y;

        if (newPosition.x < xMin || newPosition.x > xMax)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
            Movement.x *= -1;
        }

        if (newPosition.y < yMin || newPosition.y > yMax)
        {
            newPosition.y = Mathf.Clamp(newPosition.y, yMin, yMax);
            Movement.y *= -1;
        }

        transform.position = newPosition;
    }
}