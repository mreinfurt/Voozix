using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Data
{
    public class PlayerDataSaveController : MonoBehaviour
    {
        private const string GameStateFile = "/gameState.vzx";
        private const int MaximumScoreCount = 5;

        private void Start()
        {
            Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
        }

        private void Update()
        {
        }

        public static void Save(PlayerData playerData)
        {
            if (playerData.Scores.Count > MaximumScoreCount)
            {
                playerData.Scores.RemoveRange(0, playerData.Scores.Count - MaximumScoreCount);
            }

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

            if (playerData.Scores.Count > 0)
            {
                playerData.Scores.Add(0);
            }

            if (playerData.Scores.Count >= MaximumScoreCount)
            {
                playerData.Scores.RemoveAt(0);
            }

            return playerData;
        }
    }
}