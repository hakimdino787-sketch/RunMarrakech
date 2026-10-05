using UnityEngine;
namespace RunMarrakech.Player
{
    public sealed class RunnerCameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new(0f, 5.5f, -9f);
        [SerializeField] private float followSpeed = 8f;
        [SerializeField] private float lookAhead = 8f;

        private void LateUpdate()
        {
            if (target == null) return;
            Vector3 desired = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desired,
                1f - Mathf.Exp(-followSpeed * Time.deltaTime));
            transform.LookAt(target.position + Vector3.forward * lookAhead + Vector3.up * 1.2f);
        }
    }
}