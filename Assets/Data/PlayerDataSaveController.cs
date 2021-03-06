﻿#region Namespaces

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

#endregion

namespace Data
{
    /// <summary>
    /// Responsible for saving and loading the PlayerData
    /// </summary>
    public class PlayerDataSaveController : MonoBehaviour
    {
        #region Public

        private const string GameStateFile = "/gameState.vzx";

        #endregion

        #region Methods

        private void Start()
        {
            Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
        }

        private void Update()
        {
        }

        public static void Save(PlayerData playerData)
        {
            var formatter = new BinaryFormatter();
            var file = File.Create(Application.persistentDataPath + GameStateFile);

            formatter.Serialize(file, playerData);
            file.Close();
        }

        public static PlayerData Load()
        {
            if (!File.Exists(Application.persistentDataPath + GameStateFile))
            {
                return new PlayerData();
            }

            var formatter = new BinaryFormatter();
            var file = File.Open(Application.persistentDataPath + GameStateFile, FileMode.Open);
            var playerData = (PlayerData) formatter.Deserialize(file);
            file.Close();

            playerData.Score = 0;

            return playerData;
        }

        #endregion
    }
}