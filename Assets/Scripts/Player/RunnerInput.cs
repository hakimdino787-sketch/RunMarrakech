using UnityEngine;

namespace RunMarrakech.Player
{
    public sealed class RunnerInput : MonoBehaviour
    {
        public float SwipeThreshold = 80f;
        public System.Action SwipeLeft;
        public System.Action SwipeRight;
        public System.Action SwipeUp;
        public System.Action SwipeDown;

        Vector2 start;
        bool tracking;

        void Update()
        {
            if (Input.touchCount == 0) return;
            var t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began) { start = t.position; tracking = true; }
            else if (tracking && t.phase == TouchPhase.Ended)
            {
                tracking = false;
                var d = t.position - start;
                if (d.magnitude < SwipeThreshold) return;
                if (Mathf.Abs(d.x) > Mathf.Abs(d.y))
                    (d.x > 0 ? SwipeRight : SwipeLeft)?.Invoke();
                else
                    (d.y > 0 ? SwipeUp : SwipeDown)?.Invoke();
            }
        }
    }
}