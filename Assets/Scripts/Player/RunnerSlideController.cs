using UnityEngine;

namespace RunMarrakech.Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class RunnerSlideController : MonoBehaviour
    {
        public CharacterController Controller;
        public float SlideDuration = 0.65f;
        public float SlideHeight = 1.0f;
        public float NormalHeight = 2.0f;

        private float timer;
        private float originalCenterY;
        private RunnerInput runnerInput;

        private void Awake()
        {
            if (!Controller) Controller = GetComponent<CharacterController>();
            runnerInput = GetComponent<RunnerInput>();
            if (Controller)
            {
                NormalHeight = Controller.height;
                originalCenterY = Controller.center.y;
            }
        }

        private void OnEnable()
        {
            if (runnerInput == null) runnerInput = GetComponent<RunnerInput>();
            if (runnerInput != null) runnerInput.SwipeDown += Slide;
        }

        private void OnDisable()
        {
            if (runnerInput != null) runnerInput.SwipeDown -= Slide;
        }

        public void Slide()
        {
            if (!Controller || timer > 0f) return;
            timer = SlideDuration;
            Controller.height = SlideHeight;
            Controller.center = new Vector3(
                Controller.center.x,
                originalCenterY - (NormalHeight - SlideHeight) * 0.5f,
                Controller.center.z);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) Slide();
            if (timer <= 0f) return;

            timer -= Time.deltaTime;
            if (timer <= 0f && Controller)
            {
                Controller.height = NormalHeight;
                Controller.center = new Vector3(Controller.center.x, originalCenterY, Controller.center.z);
            }
        }
    }
}