using UnityEngine;
using RunMarrakech.Core;

public sealed class CoinPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<RunnerLaneController>())
        {
            GameManager.Instance?.AddCoin();
            Destroy(gameObject);
        }
    }
}