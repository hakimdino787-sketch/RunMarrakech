using UnityEngine;
using RunMarrakech.Core;

namespace RunMarrakech.Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class RunnerLaneController : MonoBehaviour
    {
        [SerializeField] private float laneWidth = 2.5f;
        [SerializeField] private float laneChangeSpeed = 14f;
        [SerializeField] private float jumpHeight = 2.2f;
        [SerializeField] private float gravity = -28f;
        [SerializeField] private float slideDuration = 0.65f;

        private CharacterController controller;
        private int laneIndex = 1;
        private float verticalVelocity;
        private float slideTimer;

        private Vector2 touchStart;
        private bool trackingTouch;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (GameManager.Instance == null ||
                GameManager.Instance.State != GameState.Playing)
                return;

            ReadInput();
            MoveRunner();
        }

        private void ReadInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                ChangeLane(-1);
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                ChangeLane(1);
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                Jump();
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                Slide();

            if (Input.touchCount == 0) return;

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
                trackingTouch = true;
            }
            else if (trackingTouch && touch.phase == TouchPhase.Ended)
            {
                Vector2 delta = touch.position - touchStart;
                trackingTouch = false;

                if (delta.magnitude < 60f) return;

                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                    ChangeLane(delta.x > 0 ? 1 : -1);
                else if (delta.y > 0)
                    Jump();
                else
                    Slide();
            }
        }

        private void ChangeLane(int direction)
        {
            laneIndex = Mathf.Clamp(laneIndex + direction, 0, 2);
        }

        private void Jump()
        {
            if (controller.isGrounded && slideTimer <= 0f)
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        private void Slide()
        {
            if (controller.isGrounded)
                slideTimer = slideDuration;
        }

        private void MoveRunner()
        {
            float targetX = (laneIndex - 1) * laneWidth;
            float deltaX = targetX - transform.position.x;
            float horizontal = Mathf.Clamp(deltaX * laneChangeSpeed, -laneChangeSpeed, laneChangeSpeed);

            if (controller.isGrounded && verticalVelocity < 0f)
                verticalVelocity = -2f;

            verticalVelocity += gravity * Time.deltaTime;
            slideTimer = Mathf.Max(0f, slideTimer - Time.deltaTime);

            Vector3 motion = new Vector3(horizontal, verticalVelocity, 0f);
            controller.Move(motion * Time.deltaTime);
        }
    }
}
