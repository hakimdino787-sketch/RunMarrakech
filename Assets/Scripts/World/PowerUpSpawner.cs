using UnityEngine;
namespace RunMarrakech.World
{
    public sealed class PowerUpSpawner : MonoBehaviour
    {
        public PowerUpPickup[] Prefabs;
        public Transform[] Lanes;
        public float Interval=18f;
        float timer;
        void Update()
        {
            if(Prefabs==null||Prefabs.Length==0||Lanes==null||Lanes.Length==0)return;
            timer+=Time.deltaTime;
            if(timer<Interval)return;
            timer=0f;
            var prefab=Prefabs[Random.Range(0,Prefabs.Length)];
            var lane=Lanes[Random.Range(0,Lanes.Length)];
            Instantiate(prefab,lane.position+Vector3.up*1.2f,Quaternion.identity);
        }
    }
}