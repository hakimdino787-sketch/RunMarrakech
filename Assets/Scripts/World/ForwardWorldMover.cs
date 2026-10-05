using UnityEngine;
using RunMarrakech.Core;

namespace RunMarrakech.World
{
    public sealed class ForwardWorldMover : MonoBehaviour
    {
        [SerializeField] private float despawnZ = -35f;

        private void Update()
        {
            if (GameManager.Instance == null ||
                GameManager.Instance.State != GameState.Playing)
                return;

            transform.Translate(
                Vector3.back * GameManager.Instance.RunSpeed * Time.deltaTime,
                Space.World);

            if (transform.position.z < despawnZ)
                Destroy(gameObject);
        }
    }
}
