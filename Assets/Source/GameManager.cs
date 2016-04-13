using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly List<GameObject> enemies = new List<GameObject>();

    public GameObject EnemyPrefab;
    public GameObject Player;

    private void Start()
    {
        var playerController = Player.GetComponent<PlayerController>();
        playerController.OnPlayerDeath += HandlePlayerDeath;
        playerController.OnStarCollected += HandleStarCollected;
    }

    private void Update()
    {
    }

    #region Events

    private void HandlePlayerDeath()
    {
        foreach (var enemy in this.enemies)
        {
            Destroy(enemy);
        }

        this.enemies.Clear();
        this.Player.GetComponent<PlayerController>().Reset();
    }

    private void HandleStarCollected()
    {
        var newEnemy = Instantiate(EnemyPrefab);
        newEnemy.SetActive(true);
        this.enemies.Add(newEnemy);
    }

    #endregion
}