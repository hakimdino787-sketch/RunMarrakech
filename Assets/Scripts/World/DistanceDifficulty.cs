using UnityEngine;
using RunMarrakech.Core;
namespace RunMarrakech.World
{
    public sealed class DistanceDifficulty:MonoBehaviour
    {
        public float BaseInterval=3f,MinInterval=.8f;
        public float CurrentInterval{get;private set;}
        void Update(){var gm=GameManager.Instance;if(!gm)return;float t=Mathf.Clamp01(gm.Distance/2500f);CurrentInterval=Mathf.Lerp(BaseInterval,MinInterval,t);}
    }
}