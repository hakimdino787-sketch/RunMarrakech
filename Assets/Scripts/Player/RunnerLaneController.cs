using UnityEngine;
using RunMarrakech.Player;

namespace RunMarrakech.Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class RunnerLaneController : MonoBehaviour
    {
        [SerializeField] private float laneWidth = 2.2f;
        [SerializeField] private float laneChangeSpeed = 12f;

        private int lane = 1;
        private RunnerInput runnerInput;

        private void Awake()
        {
            runnerInput = GetComponent<RunnerInput>();
        }

        private void OnEnable()
        {
            if (runnerInput == null) runnerInput = GetComponent<RunnerInput>();
            if (runnerInput == null) return;
            runnerInput.SwipeLeft += MoveLeft;
            runnerInput.SwipeRight += MoveRight;
        }

        private void OnDisable()
        {
            if (runnerInput == null) return;
            runnerInput.SwipeLeft -= MoveLeft;
            runnerInput.SwipeRight -= MoveRight;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) MoveLeft();
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) MoveRight();

            Vector3 position = transform.position;
            float targetX = (lane - 1) * laneWidth;
            position.x = Mathf.MoveTowards(position.x, targetX, laneChangeSpeed * Time.deltaTime);
            transform.position = position;
        }

        private void MoveLeft() => lane = Mathf.Max(0, lane - 1);
        private void MoveRight() => lane = Mathf.Min(2, lane + 1);
    }
}