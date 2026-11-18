# RUN MARRAKECH — Anime character asset pipeline

## Original character direction
- Boy: youthful adult anime athlete, black hair, turquoise jacket with restrained gold zellij details, charcoal joggers and orange running shoes.
- Girl: youthful adult anime athlete, chestnut ponytail, cream/burgundy jacket with restrained zellij details, dark joggers and cream/burgundy shoes.

Create original characters in VRoid Studio and export VRM 1.0. Install the official UniVRM Unity importer from https://github.com/vrm-c/UniVRM before importing VRM files. Check the terms for each model or item; VRoid sample models have different license conditions, so do not assume every sample is CC0.

## Required Unity paths
Import and save source prefabs as:
- `Assets/Art/Characters/Imported/RunMarrakech_AnimeRunner_Boy.prefab`
- `Assets/Art/Characters/Imported/RunMarrakech_AnimeRunner_Girl.prefab`

Place humanoid in-place animation FBX files in:
- `Assets/Art/Characters/Animations/`

Clip names should include Run/Sprint/Jog, and optionally Idle/Stand, Jump/Leap, Slide/Sliding/Crouch. Configure imported characters with valid Humanoid Avatars.

## Build runtime prefabs
In Unity choose `Tools > Run Marrakech > Build Anime Character Prefabs`. The editor importer copies both source prefabs into:
- `Assets/Resources/RunMarrakech_AnimeRunner_Boy.prefab`
- `Assets/Resources/RunMarrakech_AnimeRunner_Girl.prefab`

When a Run/Sprint/Jog clip is found, it creates a basic Animator Controller with IsRunning, IsJumping, IsSliding and RunSpeed parameters. Root motion remains disabled because lane movement is controlled by the game's CharacterController.

The start screen offers BOY RUNNER and GIRL RUNNER and stores the selected character locally. If the matching resource prefab is missing, the game safely keeps the procedural development fallback.

## Free animation reference
https://3dvalley.com/3d-animations — verify each clip's CC0 license and import it as a Humanoid animation before use.

## Verification status
The importer, selection component, and automated source checks are included in the source archive. The two original 3D models and animation FBX files have not yet been supplied/imported. Unity Editor is unavailable in the current execution environment, so prefab import, Animator retargeting, scene playtest and Android builds are not verified.