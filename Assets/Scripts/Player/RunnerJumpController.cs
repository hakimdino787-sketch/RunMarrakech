using UnityEngine;

namespace RunMarrakech.Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class RunnerJumpController : MonoBehaviour
    {
        public CharacterController Controller;
        public float JumpForce = 8.5f;
        public float Gravity = -24f;

        private float yVelocity;
        private RunnerInput runnerInput;

        private void Awake()
        {
            if (!Controller) Controller = GetComponent<CharacterController>();
            runnerInput = GetComponent<RunnerInput>();
        }

        private void OnEnable()
        {
            if (runnerInput == null) runnerInput = GetComponent<RunnerInput>();
            if (runnerInput != null) runnerInput.SwipeUp += Jump;
        }

        private void OnDisable()
        {
            if (runnerInput != null) runnerInput.SwipeUp -= Jump;
        }

        public void Jump()
        {
            if (Controller && Controller.isGrounded) yVelocity = JumpForce;
        }

        private void Update()
        {
            if (!Controller) return;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                Jump();

            if (Controller.isGrounded && yVelocity < 0f) yVelocity = -2f;
            yVelocity += Gravity * Time.deltaTime;
            Controller.Move(Vector3.up * yVelocity * Time.deltaTime);
        }
    }
}