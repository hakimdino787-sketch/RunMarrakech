using System;
using UnityEngine;
using RunMarrakech.Core;
namespace RunMarrakech.Systems
{
    public enum MissionType{Distance,Coins,Score}
    [Serializable] public class Mission{public string Id;public MissionType Type;public float Target;public bool Completed;}
    public sealed class MissionSystem:MonoBehaviour
    {
        public Mission[] Missions;
        void Start(){Load();}
        void Update()
        {
            var gm=GameManager.Instance;if(!gm||Missions==null)return;
            foreach(var m in Missions)
            {
                if(m.Completed)continue;
                float value=m.Type==MissionType.Distance?gm.Distance:m.Type==MissionType.Coins?gm.Coins:gm.Score;
                if(value>=m.Target){m.Completed=true;PlayerPrefs.SetInt("RM_M_"+m.Id,1);}
            }
            PlayerPrefs.Save();
        }
        void Load(){if(Missions==null)return;foreach(var m in Missions)m.Completed=PlayerPrefs.GetInt("RM_M_"+m.Id,0)==1;}
    }
}