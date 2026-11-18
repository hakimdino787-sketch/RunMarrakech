using UnityEngine;

namespace RunMarrakech.Player
{
    [DisallowMultipleComponent]
    public sealed class RunnerCharacterSelection : MonoBehaviour
    {
        private const string PreferenceKey = "RM_SelectedRunner";
        private const float TargetHeight = 1.82f;
        [SerializeField] private Transform visualRoot;
        private string selectedCharacter = "boy";
        public string SelectedCharacter => selectedCharacter;

        public void Configure(Transform root)
        {
            visualRoot = root;
            selectedCharacter = PlayerPrefs.GetString(PreferenceKey, "boy");
            if (selectedCharacter != "girl") selectedCharacter = "boy";
            TryApplySelectedCharacter();
        }

        public void SelectBoy() => Select("boy");
        public void SelectGirl() => Select("girl");

        public void Select(string characterId)
        {
            if (characterId != "boy" && characterId != "girl") return;
            selectedCharacter = characterId;
            PlayerPrefs.SetString(PreferenceKey, selectedCharacter);
            PlayerPrefs.Save();
            TryApplySelectedCharacter();
        }

        private bool TryApplySelectedCharacter()
        {
            if (!visualRoot) return false;
            string resourceName = selectedCharacter == "girl"
                ? "RunMarrakech_AnimeRunner_Girl"
                : "RunMarrakech_AnimeRunner_Boy";
            GameObject prefab = Resources.Load<GameObject>(resourceName);
            if (!prefab) return false;

            GameObject replacement = Instantiate(prefab, visualRoot);
            replacement.name = selectedCharacter == "girl" ? "RM_AnimeRunner_Girl" : "RM_AnimeRunner_Boy";
            replacement.transform.localPosition = Vector3.zero;
            replacement.transform.localRotation = Quaternion.identity;
            FitToHeight(replacement.transform, TargetHeight);

            for (int i = visualRoot.childCount - 1; i >= 0; i--)
            {
                Transform child = visualRoot.GetChild(i);
                if (child.gameObject != replacement) Destroy(child.gameObject);
            }

            RunnerCharacterMotion fallbackMotion = GetComponent<RunnerCharacterMotion>();
            if (fallbackMotion) Destroy(fallbackMotion);
            Animator animator = replacement.GetComponentInChildren<Animator>(true);
            if (animator) animator.applyRootMotion = false;
            if (animator && !GetComponent<RunnerCharacterAnimator>())
                gameObject.AddComponent<RunnerCharacterAnimator>();
            return true;
        }

        private static void FitToHeight(Transform character, float targetHeight)
        {
            Renderer[] renderers = character.GetComponentsInChildren<Renderer>(true);
            if (renderers.Length == 0) return;
            Bounds bounds = renderers[0].bounds;
            for (int i = 1; i < renderers.Length; i++) bounds.Encapsulate(renderers[i].bounds);
            if (bounds.size.y <= 0.001f) return;
            character.localScale *= targetHeight / bounds.size.y;
            bounds = renderers[0].bounds;
            for (int i = 1; i < renderers.Length; i++) bounds.Encapsulate(renderers[i].bounds);
            character.position += Vector3.up * (character.parent.position.y - bounds.min.y);
        }
    }
}