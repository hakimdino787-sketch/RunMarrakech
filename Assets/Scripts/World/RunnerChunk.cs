using UnityEngine;
namespace RunMarrakech.World
{
    public sealed class RunnerChunk : MonoBehaviour
    {
        [SerializeField] private Transform[] lanePoints;
        [SerializeField] private Transform[] spawnPoints;
        public Transform[] LanePoints => lanePoints;
        public Transform[] SpawnPoints => spawnPoints;
    }
}