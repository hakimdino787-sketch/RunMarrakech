using UnityEngine;
using UnityEngine.UI;
using RunMarrakech.Core;

namespace RunMarrakech.UI
{
    public sealed class RunHUD : MonoBehaviour
    {
        public Text ScoreText;
        public Text CoinsText;
        public Text HighScoreText;

        void Update()
        {
            var gm = FindFirstObjectByType<GameManager>();
            if (!gm) return;
            if (ScoreText) ScoreText.text = $"SCORE  {gm.Score:N0}";
            if (CoinsText) CoinsText.text = $"COINS  {gm.Coins:N0}";
            if (HighScoreText) HighScoreText.text = $"BEST  {gm.HighScore:N0}";
        }
    }
}