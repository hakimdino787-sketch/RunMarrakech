# RUN MARRAKECH — Unity 6 final build source

This branch stages the Unity 6 Android project source package prepared for the existing RUN MARRAKECH repository.

- Unity Editor line: `6000.0.82f1`
- Android target: portrait, ARM64
- GLB import package: `com.unity.cloud.gltfast` 6.19.0
- UI package: `com.unity.ugui` 2.0.0
- Main environment: the user's 3D Jutsu world project at https://higgsfield.ai/3d-jutsu/aa0cc6cf-7b5d-4301-9b56-88d83c763b69 (revision 2, 293 meshes)

The downloadable source archive is attached in this ChatGPT conversation as `RunMarrakech-Unity6-Source.zip`. Place the GLB at `Assets/Art/World/RM_World_v2.glb`, open the project in Unity 6, then run `Tools > Run Marrakech > Generate Final Unity Scene`.

The Android menu commands are `Tools > Run Marrakech > Build Android APK` and `Tools > Run Marrakech > Build Android AAB`. A release keystore must be configured by the project owner for store signing.

**Verification status:** source files and package metadata were statically checked. Unity Editor, Android Build Support, and a physical Android device were not available in the current execution environment, so Unity compilation, playtesting, APK/AAB generation, and device performance are not yet verified. No finished APK/AAB is claimed.
