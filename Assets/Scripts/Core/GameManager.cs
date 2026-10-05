using UnityEngine;

namespace RunMarrakech.Core
{
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameState state = GameState.Boot;

        public GameState State => state;
        public float Distance { get; private set; }
        public int Score { get; private set; }
        public int Coins { get; private set; }
        public float RunSpeed { get; private set; }

        [Header("Difficulty")]
        [SerializeField] private float startingSpeed = 8f;
        [SerializeField] private float speedIncreasePerSecond = 0.08f;
        [SerializeField] private float maxSpeed = 22f;
        [SerializeField] private int scorePerMeter = 1;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            SetState(GameState.Menu);
        }

        private void Update()
        {
            if (state != GameState.Playing) return;

            RunSpeed = Mathf.Min(maxSpeed, RunSpeed + speedIncreasePerSecond * Time.deltaTime);
            Distance += RunSpeed * Time.deltaTime;
            Score = Mathf.FloorToInt(Distance * scorePerMeter);
        }

        public void StartRun()
        {
            Distance = 0f;
            Score = 0;
            Coins = 0;
            RunSpeed = startingSpeed;
            SetState(GameState.Playing);
        }

        public void AddCoins(int amount)
        {
            if (amount > 0) Coins += amount;
        }

        public void PauseRun()
        {
            if (state == GameState.Playing) SetState(GameState.Paused);
        }

        public void ResumeRun()
        {
            if (state == GameState.Paused) SetState(GameState.Playing);
        }

        public void EndRun()
        {
            if (state == GameState.Playing || state == GameState.Paused)
                SetState(GameState.GameOver);
        }

        private void SetState(GameState next)
        {
            state = next;
        }
    }
}
