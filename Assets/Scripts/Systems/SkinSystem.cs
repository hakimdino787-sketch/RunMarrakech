using UnityEngine;
namespace RunMarrakech.Systems
{
    public sealed class SkinSystem:MonoBehaviour
    {
        public Renderer CharacterRenderer;
        public Material[] Skins;
        public int Selected{get;private set;}
        void Awake(){Selected=Mathf.Clamp(PlayerPrefs.GetInt("RM_Skin",0),0,Mathf.Max(0,(Skins?.Length??1)-1));Apply();}
        public void Select(int index){if(Skins==null||index<0||index>=Skins.Length)return;Selected=index;PlayerPrefs.SetInt("RM_Skin",index);PlayerPrefs.Save();Apply();}
        void Apply(){if(CharacterRenderer&&Skins!=null&&Skins.Length>0)CharacterRenderer.sharedMaterial=Skins[Selected];}
    }
}