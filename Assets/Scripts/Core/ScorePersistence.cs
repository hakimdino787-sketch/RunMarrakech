using UnityEngine;
namespace RunMarrakech.Core
{
    public sealed class ScorePersistence : MonoBehaviour
    {
        public void SaveRun()
        {
            if (GameManager.Instance == null) return;
            RunSaveData data = RunSaveSystem.Load();
            data.highScore = Mathf.Max(data.highScore, GameManager.Instance.Score);
            data.totalCoins += GameManager.Instance.Coins;
            RunSaveSystem.Save(data);
        }
    }
}