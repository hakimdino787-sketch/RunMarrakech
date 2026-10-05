using UnityEngine;
namespace RunMarrakech.World
{
    public sealed class CoinLineSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private float laneWidth = 2.5f;
        [SerializeField] private float spacing = 2.2f;
        [SerializeField] private int count = 8;
        [SerializeField] private float height = 1.1f;

        public void SpawnLine(int lane, Vector3 start)
        {
            if (coinPrefab == null) return;
            float x = (Mathf.Clamp(lane, 0, 2) - 1) * laneWidth;
            for (int i = 0; i < count; i++)
                Instantiate(coinPrefab, start + new Vector3(x, height, i * spacing),
                    Quaternion.identity, transform);
        }
    }
}