using System.Collections.Generic;
using UnityEngine;
using RunMarrakech.Core;

namespace RunMarrakech.World
{
    public sealed class EndlessChunkSpawner : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private RunnerChunk[] chunkPrefabs;
        [SerializeField] private float chunkLength = 30f;
        [SerializeField] private int initialChunks = 8;
        [SerializeField] private float spawnAheadDistance = 150f;
        [SerializeField] private float cleanupBehindDistance = 70f;

        private readonly Queue<RunnerChunk> activeChunks = new();
        private float nextSpawnZ;

        private void Start()
        {
            if (chunkPrefabs == null || chunkPrefabs.Length == 0) return;
            for (int i = 0; i < initialChunks; i++) SpawnChunk();
            nextSpawnZ = initialChunks * chunkLength;
        }

        private void Update()
        {
            if (player == null || GameManager.Instance == null ||
                GameManager.Instance.State != GameState.Playing) return;

            while (nextSpawnZ < player.position.z + spawnAheadDistance) SpawnChunk();

            while (activeChunks.Count > 1 &&
                   activeChunks.Peek().transform.position.z < player.position.z - cleanupBehindDistance)
                Destroy(activeChunks.Dequeue().gameObject);
        }

        private void SpawnChunk()
        {
            RunnerChunk prefab = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
            RunnerChunk chunk = Instantiate(prefab,
                new Vector3(0f, 0f, nextSpawnZ), Quaternion.identity, transform);
            activeChunks.Enqueue(chunk);
            nextSpawnZ += chunkLength;
        }
    }
}