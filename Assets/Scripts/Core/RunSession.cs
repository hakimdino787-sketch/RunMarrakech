using UnityEngine;
namespace RunMarrakech.Core
{
    public sealed class RunSession:MonoBehaviour
    {
        public static RunSession Instance{get;private set;}
        public bool IsRunning{get;private set;}
        void Awake(){if(Instance!=null){Destroy(gameObject);return;}Instance=this;DontDestroyOnLoad(gameObject);}
        public void StartRun(){Time.timeScale=1f;IsRunning=true;}
        public void EndRun(){IsRunning=false;GameManager.Instance?.SaveRun();}
    }
}