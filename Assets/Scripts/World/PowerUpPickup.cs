using UnityEngine;
using RunMarrakech.Core;

namespace RunMarrakech.World
{
    public sealed class PowerUpPickup : MonoBehaviour
    {
        public PowerUpType Type;
        public float Duration = 7f;
        public float RotationSpeed = 90f;

        void Update() => transform.Rotate(0f, RotationSpeed * Time.deltaTime, 0f, Space.World);

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            var gm = FindFirstObjectByType<GameManager>();
            if (gm) gm.ActivatePowerUp(Type, Duration);
            Destroy(gameObject);
        }
    }
}