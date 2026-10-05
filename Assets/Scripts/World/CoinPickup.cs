using UnityEngine;
using RunMarrakech.Core;

namespace RunMarrakech.World
{
    public sealed class CoinPickup : MonoBehaviour
    {
        [SerializeField] private int value = 1;
        [SerializeField] private float rotationSpeed = 180f;

        private void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            GameManager.Instance?.AddCoins(value);
            Destroy(gameObject);
        }
    }
}
