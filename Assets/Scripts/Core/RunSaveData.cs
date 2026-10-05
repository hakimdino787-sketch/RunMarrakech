using System;
using UnityEngine;

namespace RunMarrakech.Core
{
    [Serializable]
    public sealed class RunSaveData
    {
        public int highScore;
        public int totalCoins;
        public int selectedSkin;
    }

    public static class RunSaveSystem
    {
        private const string SaveKey = "RUN_MARRAKECH_SAVE";

        public static RunSaveData Load()
        {
            string json = PlayerPrefs.GetString(SaveKey, string.Empty);
            return string.IsNullOrEmpty(json) ? new RunSaveData() : JsonUtility.FromJson<RunSaveData>(json);
        }

        public static void Save(RunSaveData data)
        {
            PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }
    }
}