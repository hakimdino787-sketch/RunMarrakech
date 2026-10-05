using UnityEngine;
namespace RunMarrakech.World
{
    public sealed class ObstaclePattern : MonoBehaviour
    {
        [SerializeField] private GameObject obstaclePrefab;
        [SerializeField] private float laneWidth = 2.5f;
        [SerializeField] private float[] distances = { 8f, 16f, 24f };

        public void BuildPattern(int blockedLane, Vector3 origin)
        {
            if (obstaclePrefab == null) return;
            int lane = Mathf.Clamp(blockedLane, 0, 2);
            foreach (float distance in distances)
            {
                float x = (lane - 1) * laneWidth;
                Instantiate(obstaclePrefab, origin + new Vector3(x, 0f, distance),
                    Quaternion.identity, transform);
            }
        }
    }
}