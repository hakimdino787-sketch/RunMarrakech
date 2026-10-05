using UnityEngine;
using RunMarrakech.Core;
namespace RunMarrakech.World
{
    public sealed class ObstacleCollision : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Player")) return;
            var gm=GameManager.Instance;
            if(gm && gm.ShieldActive){Destroy(gameObject);return;}
            Time.timeScale=0f;
        }
    }
}