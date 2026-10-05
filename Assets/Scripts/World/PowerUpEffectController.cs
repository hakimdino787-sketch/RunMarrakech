using UnityEngine;
using RunMarrakech.Core;

namespace RunMarrakech.World
{
    public sealed class PowerUpEffectController : MonoBehaviour
    {
        public float SpeedMultiplier=1.45f;
        public float MagnetRadius=5f;
        public float CoinPullSpeed=18f;

        void Update()
        {
            var gm=GameManager.Instance;
            if(!gm || !gm.MagnetActive) return;
            var coins=GameObject.FindGameObjectsWithTag("Coin");
            foreach(var coin in coins)
            {
                float d=Vector3.Distance(transform.position,coin.transform.position);
                if(d<=MagnetRadius)
                    coin.transform.position=Vector3.MoveTowards(coin.transform.position,transform.position,CoinPullSpeed*Time.deltaTime);
            }
        }
    }
}