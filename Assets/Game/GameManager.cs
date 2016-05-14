using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private readonly List<GameObject> enemies = new List<GameObject>();

        public GameObject EnemyPrefab;
        public GameObject Player;

        private void Start()
        {
            var playerController = this.Player.GetComponent<PlayerController>();
            playerController.OnPlayerDeath += this.HandlePlayerDeath;
            playerController.OnStarCollected += this.HandleStarCollected;
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
            var newEnemy = Instantiate(this.EnemyPrefab);
            newEnemy.SetActive(true);
            this.enemies.Add(newEnemy);
        }

        #endregion
    }
}