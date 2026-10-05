#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using RunMarrakech.Core;
using RunMarrakech.Player;
using RunMarrakech.World;
using RunMarrakech.UI;
using RunMarrakech.Audio;

namespace RunMarrakech.Editor
{
    public static class RunMarrakechProjectSetup
    {
        const string ScenePath="Assets/Scenes/RunMarrakech.unity";

        [MenuItem("Tools/Run Marrakech/Create Starter Scene")]
        public static void CreateStarterScene()
        {
            EnsureFolder("Assets/Scenes");
            EnsureFolder("Assets/Materials");
            var scene=EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,NewSceneMode.Single);

            var gm=new GameObject("GameManager");
            gm.AddComponent<GameManager>();
            gm.AddComponent<RunSession>();
            gm.AddComponent<MissionSystem>();

            var light=new GameObject("Sun");
            var dl=light.AddComponent<Light>(); dl.type=LightType.Directional; dl.intensity=1.2f;
            light.transform.rotation=Quaternion.Euler(45f,-25f,0f);

            CreateRoad();
            var player=CreatePlayer();
            CreateCamera(player.transform);
            CreateHUD();
            CreateWorldSamples();

            EditorSceneManager.SaveScene(scene,ScenePath);
            Selection.activeGameObject=player;
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("RUN MARRAKECH starter scene created: "+ScenePath);
        }

        static GameObject CreatePlayer()
        {
            var p=GameObject.CreatePrimitive(PrimitiveType.Capsule);
            p.name="Player_Runner";
            p.tag="Player";
            p.transform.position=new Vector3(0,1.1f,0);
            Object.DestroyImmediate(p.GetComponent<Collider>());
            var cc=p.AddComponent<CharacterController>(); cc.height=2f; cc.radius=.42f;
            p.AddComponent<RunnerLaneController>();
            p.AddComponent<RunnerJumpController>();
            p.AddComponent<RunnerSlideController>();
            p.AddComponent<RunnerInput>();
            return p;
        }

        static void CreateCamera(Transform target)
        {
            var go=new GameObject("Main Camera");
            go.tag="MainCamera";
            var cam=go.AddComponent<Camera>();
            go.AddComponent<AudioListener>();
            go.transform.position=new Vector3(0,5.2f,-8f);
            go.transform.LookAt(target.position+Vector3.up*1.2f);
            var follow=go.AddComponent<RunnerCameraFollow>();
            follow.Target=target;
            follow.Offset=new Vector3(0,4.5f,-7f);
        }

        static void CreateRoad()
        {
            var road=GameObject.CreatePrimitive(PrimitiveType.Cube);
            road.name="Marrakech_Road";
            road.transform.position=new Vector3(0,-.15f,80);
            road.transform.localScale=new Vector3(7f,.3f,220f);
            var mat=MakeMaterial("Road",new Color(.12f,.1f,.09f)); road.GetComponent<Renderer>().sharedMaterial=mat;
            for(int lane=0;lane<2;lane++)
            {
                var line=GameObject.CreatePrimitive(PrimitiveType.Cube);
                line.name="LaneMarker_"+lane;
                line.transform.position=new Vector3((lane==0?-1.1f:1.1f),.03f,80);
                line.transform.localScale=new Vector3(.06f,.03f,220f);
                line.GetComponent<Renderer>().sharedMaterial=MakeMaterial("Lane",new Color(.85f,.7f,.35f));
            }
        }

        static void CreateWorldSamples()
        {
            for(int z=20;z<=180;z+=40)
            {
                CreateBuilding(-7f,z); CreateBuilding(7f,z);
                CreatePalm(-5f,z+10); CreatePalm(5f,z+20);
            }
            for(int z=25;z<180;z+=18)
            {
                CreateCoin(new Vector3(0,1.2f,z));
                if(z%36==7) CreateBarrier(new Vector3(-2.2f,.7f,z+8));
            }
        }

        static void CreateBuilding(float x,float z)
        {
            var b=GameObject.CreatePrimitive(PrimitiveType.Cube);
            b.name="Riad_Block";
            b.transform.position=new Vector3(x,2.5f,z);
            b.transform.localScale=new Vector3(3.5f,5f,10f);
            b.GetComponent<Renderer>().sharedMaterial=MakeMaterial("Ochre",new Color(.55f,.28f,.16f));
        }

        static void CreatePalm(float x,float z)
        {
            var trunk=GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            trunk.name="Palm_Trunk"; trunk.transform.position=new Vector3(x,2f,z);
            trunk.transform.localScale=new Vector3(.25f,2f,.25f);
            var crown=GameObject.CreatePrimitive(PrimitiveType.Sphere);
            crown.name="Palm_Crown"; crown.transform.position=new Vector3(x,4.2f,z);
            crown.transform.localScale=new Vector3(1.3f,.5f,1.3f);
        }

        static void CreateCoin(Vector3 pos)
        {
            var c=GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            c.name="Coin"; c.transform.position=pos; c.transform.localScale=new Vector3(.35f,.08f,.35f);
            c.transform.rotation=Quaternion.Euler(90,0,0);
            var col=c.GetComponent<Collider>(); col.isTrigger=true;
            c.AddComponent<CoinPickup>();
        }

        static void CreateBarrier(Vector3 pos)
        {
            var b=GameObject.CreatePrimitive(PrimitiveType.Cube);
            b.name="Barrier"; b.transform.position=pos; b.transform.localScale=new Vector3(1.6f,1.4f,.8f);
            var col=b.GetComponent<Collider>(); col.isTrigger=true;
            b.AddComponent<ObstacleCollision>();
        }

        static void CreateHUD()
        {
            var canvas=new GameObject("HUD");
            var c=canvas.AddComponent<Canvas>(); c.renderMode=RenderMode.ScreenSpaceOverlay;
            canvas.AddComponent<CanvasScaler>(); canvas.AddComponent<GraphicRaycaster>();
            var score=MakeText(canvas.transform,"Score",new Vector2(180,-60));
            var coins=MakeText(canvas.transform,"Coins",new Vector2(180,-105));
            var best=MakeText(canvas.transform,"Best",new Vector2(180,-150));
            var hud=canvas.AddComponent<RunHUD>(); hud.ScoreText=score; hud.CoinsText=coins; hud.HighScoreText=best;
        }

        static Text MakeText(Transform parent,string name,Vector2 pos)
        {
            var go=new GameObject(name); go.transform.SetParent(parent,false);
            var t=go.AddComponent<Text>(); t.font=Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); t.fontSize=30; t.alignment=TextAnchor.MiddleLeft;
            var rt=t.rectTransform; rt.anchorMin=new Vector2(0,1); rt.anchorMax=new Vector2(0,1); rt.anchoredPosition=pos; rt.sizeDelta=new Vector2(500,45);
            t.text=name.ToUpper()+"  0"; return t;
        }

        static Material MakeMaterial(string name,Color color)
        {
            var path="Assets/Materials/"+name+".mat";
            var m=AssetDatabase.LoadAssetAtPath<Material>(path);
            if(!m){m=new Material(Shader.Find("Universal Render Pipeline/Lit")); m.name=name; AssetDatabase.CreateAsset(m,path);}
            m.color=color; return m;
        }

        static void EnsureFolder(string path){if(!AssetDatabase.IsValidFolder(path)){var parent=Path.GetDirectoryName(path).Replace("\\","/");var folder=Path.GetFileName(path);AssetDatabase.CreateFolder(parent,folder);}}
    }
}
#endif
