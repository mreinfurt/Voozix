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
            Events.Player.OnDeath += this.HandlePlayerDeath;
            Events.Player.OnStarCollected += this.HandleStarCollected;
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
        }

        private void HandleStarCollected()
        {
            var newEnemy = Instantiate(this.EnemyPrefab);
            newEnemy.SetActive(true);
            newEnemy.GetComponent<EnemyController>().Spawn(this.Player.transform.position);
            this.enemies.Add(newEnemy);
        }

        #endregion
    }
}