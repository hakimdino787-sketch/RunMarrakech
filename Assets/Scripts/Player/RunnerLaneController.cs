using UnityEngine;
using RunMarrakech.Core;

public sealed class RunnerLaneController : MonoBehaviour
{
    [SerializeField] private float laneWidth = 2.2f;
    [SerializeField] private float laneChangeSpeed = 12f;
    private int lane = 1;
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float targetX = (lane - 1) * laneWidth;
        float deltaX = Mathf.MoveTowards(transform.position.x, targetX, laneChangeSpeed * Time.deltaTime) - transform.position.x;
        float forwardSpeed = GameManager.Instance != null ? GameManager.Instance.RunSpeed : 8f;
        Vector3 motion = new Vector3(deltaX, 0f, forwardSpeed * Time.deltaTime);
        if (controller != null) controller.Move(motion);
        else transform.position += motion;
    }

    public void ChangeLane(int direction)
    {
        if (direction > 0) lane = Mathf.Min(2, lane + 1);
        else if (direction < 0) lane = Mathf.Max(0, lane - 1);
    }
}