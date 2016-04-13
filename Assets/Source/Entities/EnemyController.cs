using System;
using UnityEngine;
using Random = UnityEngine.Random;

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

        this.GetComponent<ClampToScreenBounds>().OnHitScreenBounds += OnHitScreenBounds;
    }

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

        this.transform.position += position;
    }
}