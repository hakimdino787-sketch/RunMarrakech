# RUN MARRAKECH — Unity 6 final build source

This branch stages the Unity 6 Android source package and the latest anime runner integration helpers for the existing RUN MARRAKECH repository.

- Unity Editor line: `6000.0.82f1`
- Android target: portrait, ARM64
- GLB import package: `com.unity.cloud.gltfast` 6.19.0
- UI package: `com.unity.ugui` 2.0.0
- Main environment: user's 3D Jutsu world project (revision 2, 293 meshes): https://higgsfield.ai/3d-jutsu/aa0cc6cf-7b5d-4301-9b56-88d83c763b69

The complete portable Unity source archive is attached to the current ChatGPT conversation as `RunMarrakech-Unity6-Source.zip`. Place the world GLB at `Assets/Art/World/RM_World_v2.glb`, open the project in Unity 6, then run `Tools > Run Marrakech > Generate Final Unity Scene`.

## Anime runner update

The current source archive includes persistent Boy/Girl selection, a Resources-prefab loader, and an Editor importer at `Tools > Run Marrakech > Build Anime Character Prefabs`. The two actual source models and their animation clips still need to be created/imported under the paths in `Docs/ANIME_CHARACTER_PIPELINE.md`. Until then, the development fallback remains active.

## Android output

The Android menu commands are `Tools > Run Marrakech > Build Android APK` and `Tools > Run Marrakech > Build Android AAB`. A release keystore must be configured by the project owner for store signing.

## Verification status

The current source archive passed 7 automated Python source-integration checks, package-manifest JSON parsing, raw C# brace-balance checks and ZIP integrity validation. Unity Editor, Android Build Support and a physical Android device were not available in the current execution environment, so Unity compilation, prefab import, animation retargeting, scene playtesting, APK/AAB generation and device performance are not yet verified. No finished APK/AAB is claimed.