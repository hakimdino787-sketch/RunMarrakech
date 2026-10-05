using System.Collections.Generic;
using UnityEngine;
using RunMarrakech.World;

namespace RunMarrakech.Core
{
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [SerializeField] float startSpeed = 8f, maxSpeed = 22f, acceleration = .35f;
        public float RunSpeed { get; private set; }
        public float Distance { get; private set; }
        public int Coins { get; private set; }
        public int HighScore { get; private set; }
        public float ScoreMultiplier { get; private set; } = 1f;
        public bool MagnetActive { get; private set; }
        public bool ShieldActive { get; private set; }
        public bool SpeedBoostActive { get; private set; }
        public int Score => Mathf.FloorToInt(Distance * 2f * ScoreMultiplier) + Coins * 10;

        void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            RunSpeed = startSpeed;
            HighScore = PlayerPrefs.GetInt("RM_HighScore", 0);
        }

        void Update()
        {
            if (Time.timeScale == 0) return;
            RunSpeed = Mathf.Min(maxSpeed, RunSpeed + acceleration * Time.deltaTime);
            Distance += RunSpeed * Time.deltaTime;
            if (Score > HighScore) HighScore = Score;
        }

        public void AddCoin(int amount = 1) => Coins += amount;

        public void ActivatePowerUp(PowerUpType type, float duration)
        {
            switch (type)
            {
                case PowerUpType.Magnet: MagnetActive = true; break;
                case PowerUpType.Shield: ShieldActive = true; break;
                case PowerUpType.ScoreMultiplier: ScoreMultiplier = 2f; break;
                case PowerUpType.SpeedBoost: SpeedBoostActive = true; break;
            }
            CancelInvoke(nameof(ClearPowerUps));
            Invoke(nameof(ClearPowerUps), duration);
        }

        void ClearPowerUps()
        {
            MagnetActive = false;
            ShieldActive = false;
            ScoreMultiplier = 1f;
            SpeedBoostActive = false;
        }

        public void SaveRun()
        {
            PlayerPrefs.SetInt("RM_HighScore", HighScore);
            PlayerPrefs.Save();
        }

        void OnApplicationPause(bool paused) { if (paused) SaveRun(); }
        void OnApplicationQuit() => SaveRun();
    }
}