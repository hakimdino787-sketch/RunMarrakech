# Production world asset import

The production Marrakech environment is stored in the user's 3D Jutsu project:

- Project: https://higgsfield.ai/3d-jutsu/aa0cc6cf-7b5d-4301-9b56-88d83c763b69
- Asset: revision 2 GLB, 2,238,604 bytes, 293 mesh objects
- Intended local path: `Assets/Art/World/RM_World_v2.glb`

Download the GLB from the project page and copy it to the path above. Unity glTFast imports GLB assets into the Project window. Then run `Tools > Run Marrakech > Generate Final Unity Scene`.

If the GLB is absent, the scene generator creates a playable procedural Marrakech fallback so gameplay can still be tested. The final art scene should use the original GLB.
