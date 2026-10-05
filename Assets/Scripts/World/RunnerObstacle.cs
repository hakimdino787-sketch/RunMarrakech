using UnityEngine;
public sealed class RunnerObstacle:MonoBehaviour{void OnTriggerEnter(Collider other){if(other.GetComponentInParent<RunnerLaneController>())Time.timeScale=0;}}