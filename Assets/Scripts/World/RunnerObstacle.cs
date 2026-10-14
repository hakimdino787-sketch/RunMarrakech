using UnityEngine;
using RunMarrakech.Core;

public sealed class RunnerObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<RunnerLaneController>() == null) return;
        GameManager.Instance?.EndRun();
        if (GameManager.Instance == null) Time.timeScale = 0f;
    }
}