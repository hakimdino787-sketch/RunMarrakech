using UnityEngine;

namespace RunMarrakech.Player
{
    public sealed class RunnerSlideController : MonoBehaviour
    {
        public CharacterController Controller;
        public float SlideDuration = 0.65f;
        public float SlideHeight = 1.0f;
        public float NormalHeight = 2.0f;

        float timer;
        float originalCenterY;

        void Awake()
        {
            if (!Controller) Controller = GetComponent<CharacterController>();
            if (Controller) { NormalHeight = Controller.height; originalCenterY = Controller.center.y; }
        }

        public void Slide()
        {
            if (!Controller || timer > 0f) return;
            timer = SlideDuration;
            Controller.height = SlideHeight;
            Controller.center = new Vector3(Controller.center.x, originalCenterY - (NormalHeight - SlideHeight) * .5f, Controller.center.z);
        }

        void Update()
        {
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