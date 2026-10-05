using UnityEngine;
using RunMarrakech.Core;

namespace RunMarrakech.World
{
    public sealed class RunnerObstacle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            GameManager.Instance?.EndRun();
        }
    }
}
