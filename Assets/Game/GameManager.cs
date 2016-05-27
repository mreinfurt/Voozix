#region Namespaces

using System.Collections.Generic;
using Data;
using Entities;
using UnityEngine;

#endregion

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        #region Fields

        private readonly List<GameObject> enemies = new List<GameObject>();

        public GameObject EnemyPrefab;
        public GameObject Player;

        #endregion

        #region Methods

        private void Start()
        {
            Events.Player.OnDeath += this.HandlePlayerDeath;
            Events.Player.OnStarCollected += this.HandleStarCollected;
        }

        private void Update()
        {
        }

        #endregion

        #region Events

        private void HandlePlayerDeath(PlayerData data)
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