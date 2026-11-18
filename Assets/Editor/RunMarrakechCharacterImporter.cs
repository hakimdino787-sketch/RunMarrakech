#if UNITY_EDITOR
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace RunMarrakech.Editor
{
    public static class RunMarrakechCharacterImporter
    {
        private const string SourceFolder = "Assets/Art/Characters/Imported";
        private const string AnimationFolder = "Assets/Art/Characters/Animations";
        private const string ResourcesFolder = "Assets/Resources";
        private const string ControllerPath = "Assets/Art/Characters/RunMarrakech_AnimeRunner.controller";

        [MenuItem("Tools/Run Marrakech/Build Anime Character Prefabs")]
        public static void BuildCharacterPrefabs()
        {
            EnsureFolder("Assets/Art");
            EnsureFolder("Assets/Art/Characters");
            EnsureFolder(SourceFolder);
            EnsureFolder(AnimationFolder);
            EnsureFolder(ResourcesFolder);
            int built = BuildOne("Boy", "RunMarrakech_AnimeRunner_Boy")
                      + BuildOne("Girl", "RunMarrakech_AnimeRunner_Girl");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            string message = built == 2
                ? "Both anime runner prefabs were created in Assets/Resources."
                : "Created " + built + " of 2 prefabs. Import both source prefabs into " + SourceFolder + " first.";
            Debug.Log("RUN MARRAKECH character import: " + message);
            EditorUtility.DisplayDialog("RUN MARRAKECH Characters", message, "OK");
        }

        private static int BuildOne(string label, string resourceName)
        {
            string sourcePath = SourceFolder + "/" + resourceName + ".prefab";
            GameObject source = AssetDatabase.LoadAssetAtPath<GameObject>(sourcePath);
            if (!source) { Debug.LogWarning("Missing " + label + " source prefab: " + sourcePath); return 0; }
            GameObject instance = PrefabUtility.InstantiatePrefab(source) as GameObject;
            if (!instance) return 0;
            try
            {
                instance.name = resourceName;
                Animator animator = instance.GetComponentInChildren<Animator>(true);
                if (!animator) Debug.LogWarning(label + " prefab has no Animator.");
                else
                {
                    animator.applyRootMotion = false;
                    if (!animator.avatar || !animator.avatar.isValid || !animator.avatar.isHuman)
                        Debug.LogWarning(label + " Animator Avatar is not a valid Humanoid rig.");
                    if (animator.runtimeAnimatorController == null)
                    {
                        RuntimeAnimatorController controller = BuildController();
                        if (controller) animator.runtimeAnimatorController = controller;
                    }
                }
                string destination = ResourcesFolder + "/" + resourceName + ".prefab";
                PrefabUtility.SaveAsPrefabAsset(instance, destination);
                Debug.Log("Created runner prefab: " + destination);
                return 1;
            }
            finally { Object.DestroyImmediate(instance); }
        }

        private static RuntimeAnimatorController BuildController()
        {
            AnimationClip[] clips = AssetDatabase.FindAssets("t:AnimationClip", new[] { AnimationFolder })
                .Select(AssetDatabase.GUIDToAssetPath)
                .SelectMany(path => AssetDatabase.LoadAllAssetsAtPath(path).OfType<AnimationClip>())
                .Where(clip => clip && !clip.name.StartsWith("__preview__"))
                .GroupBy(clip => clip.name).Select(group => group.First()).ToArray();
            AnimationClip run = FindClip(clips, "run", "sprint", "jog");
            if (!run) { Debug.LogWarning("No Run/Sprint/Jog clip found in " + AnimationFolder); return null; }
            AnimationClip idle = FindClip(clips, "idle", "stand") ?? run;
            AnimationClip jump = FindClip(clips, "jump", "leap");
            AnimationClip slide = FindClip(clips, "slide", "sliding", "crouch");
            if (AssetDatabase.LoadAssetAtPath<AnimatorController>(ControllerPath)) AssetDatabase.DeleteAsset(ControllerPath);
            AnimatorController controller = AnimatorController.CreateAnimatorControllerAtPath(ControllerPath);
            controller.AddParameter("IsRunning", AnimatorControllerParameterType.Bool);
            controller.AddParameter("IsJumping", AnimatorControllerParameterType.Bool);
            controller.AddParameter("IsSliding", AnimatorControllerParameterType.Bool);
            controller.AddParameter("RunSpeed", AnimatorControllerParameterType.Float);
            AnimatorStateMachine machine = controller.layers[0].stateMachine;
            AnimatorState idleState = machine.AddState("Idle");
            idleState.motion = idle;
            machine.defaultState = idleState;
            AnimatorState runState = machine.AddState("Run");
            runState.motion = run;
            AnimatorStateTransition toRun = idleState.AddTransition(runState);
            toRun.hasExitTime = false;
            toRun.AddCondition(AnimatorConditionMode.If, 0f, "IsRunning");
            AnimatorStateTransition toIdle = runState.AddTransition(idleState);
            toIdle.hasExitTime = false;
            toIdle.AddCondition(AnimatorConditionMode.IfNot, 0f, "IsRunning");
            if (jump)
            {
                AnimatorState state = machine.AddState("Jump"); state.motion = jump;
                AnimatorStateTransition enter = runState.AddTransition(state);
                enter.hasExitTime = false; enter.AddCondition(AnimatorConditionMode.If, 0f, "IsJumping");
                AnimatorStateTransition exit = state.AddTransition(runState);
                exit.hasExitTime = true; exit.exitTime = 0.9f;
            }
            if (slide)
            {
                AnimatorState state = machine.AddState("Slide"); state.motion = slide;
                AnimatorStateTransition enter = runState.AddTransition(state);
                enter.hasExitTime = false; enter.AddCondition(AnimatorConditionMode.If, 0f, "IsSliding");
                AnimatorStateTransition exit = state.AddTransition(runState);
                exit.hasExitTime = true; exit.exitTime = 0.9f;
            }
            EditorUtility.SetDirty(controller);
            return controller;
        }

        private static AnimationClip FindClip(AnimationClip[] clips, params string[] tokens) =>
            clips.FirstOrDefault(clip => tokens.Any(token => clip.name.ToLowerInvariant().Contains(token)));

        private static void EnsureFolder(string path)
        {
            if (AssetDatabase.IsValidFolder(path)) return;
            string parent = Path.GetDirectoryName(path)?.Replace('\\', '/');
            if (!string.IsNullOrEmpty(parent) && AssetDatabase.IsValidFolder(parent))
                AssetDatabase.CreateFolder(parent, Path.GetFileName(path));
        }
    }
}
#endif