# RUN MARRAKECH — Anime character asset pipeline

## Chosen free workflow

Use **VRoid Studio** to create the two original stylized anime runners (boy and girl), export each as VRM 1.0, then import the VRM files into this Unity 6 project with **UniVRM**.

- VRoid Studio: https://vroid.com/en/studio
- VRoid export guide: https://vroid.pixiv.help/hc/en-us/articles/15760756822297-I-want-to-learn-more-about-the-VRM-export-feature
- VRoid usage guidelines: https://vroid.com/en/studio/guidelines
- UniVRM releases and Unity compatibility: https://github.com/vrm-c/UniVRM

This route is selected because it produces actual anime-style humanoid 3D models with a humanoid skeleton, without requiring paid text-to-3D credits. The VRoid preset assets are licensed content, not CC0: check each item's special license terms before using it. Original hair, textures, clothing and accessories can be customized for the RUN MARRAKECH look.

## Character art direction

Create two distinct original characters:
- **Boy runner:** youthful adult anime athlete, black hair, turquoise/teal Moroccan-inspired jacket with restrained gold zellij accents, charcoal joggers, orange running shoes.
- **Girl runner:** youthful adult anime athlete, chestnut ponytail, cream-and-burgundy athletic jacket with restrained zellij accents, dark joggers, cream/burgundy running shoes.

Keep both characters recognizably adult, full-body, with readable silhouettes and mobile-friendly geometry. Use original designs; do not copy existing anime characters.

## Unity import steps

1. Install a current UniVRM release compatible with Unity 2022.3 LTS or later (the project targets Unity 6). Prefer the official Unity package/release instructions.
2. Export both characters from VRoid Studio as **VRM 1.0**.
3. Import the VRM files into Unity. Confirm each imported character has a valid humanoid skeleton/Avatar and an Animator.
4. Add a compatible humanoid animation controller with looping Run and one-shot Jump/Slide clips. Keep root motion disabled because the runner's lane movement is controlled by the game's CharacterController.
5. Create a prefab for each character and place them under `Assets/Resources/`:
   - `RunMarrakech_AnimeRunner_Boy.prefab`
   - `RunMarrakech_AnimeRunner_Girl.prefab`
6. The current scene bootstrap looks for `Resources/RunMarrakech_AnimeRunner`. Until the character-selection system is added, create a selected default prefab with that exact resource name, or update the bootstrap lookup to the selected boy/girl resource.
7. Check the model's scale, feet contact, forward direction, materials, mobile texture sizes and draw calls. The bootstrap fits the imported visual to about 1.82 m.

## Animation notes

- Use humanoid clips compatible with the imported Avatar. In-place run cycles are preferred; the game itself advances the player.
- Jump and slide clips should be configured as one-shot states, with transitions driven by the existing `RunnerCharacterAnimator` parameters (`IsRunning`, `IsJumping`, `IsSliding`).
- Do not enable Animator root motion.
- Test both characters in the generated scene before treating either as production-ready.

## Current status

This is an integration plan and asset specification only. The VRoid characters have **not** yet been created or imported, and no VRM/FBX/GLB character asset is included in the repository. The prior generated concept images are not rigged 3D models. Unity compilation, animation retargeting and Android device performance still require Unity Editor verification.
