using UnityEngine;

namespace RunMarrakech.Player
{
    public sealed class RunnerInput : MonoBehaviour
    {
        [SerializeField] private float swipeThreshold = 80f;
        private Vector2 start;
        private bool tracking;
        private RunnerLaneController lanes;
        private RunnerJumpController jump;
        private RunnerSlideController slide;

        private void Awake()
        {
            lanes = GetComponent<RunnerLaneController>();
            jump = GetComponent<RunnerJumpController>();
            slide = GetComponent<RunnerSlideController>();
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    start = touch.position;
                    tracking = true;
                }
                else if (tracking && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
                {
                    tracking = false;
                    HandleSwipe(touch.position - start);
                }
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                start = Input.mousePosition;
                tracking = true;
            }
            else if (tracking && Input.GetMouseButtonUp(0))
            {
                tracking = false;
                HandleSwipe((Vector2)Input.mousePosition - start);
            }
        }

        private void HandleSwipe(Vector2 delta)
        {
            if (delta.magnitude < swipeThreshold) return;
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                if (lanes != null) lanes.ChangeLane(delta.x > 0f ? 1 : -1);
            }
            else if (delta.y > 0f)
            {
                if (jump != null) jump.Jump();
            }
            else
            {
                if (slide != null) slide.Slide();
            }
        }
    }
}