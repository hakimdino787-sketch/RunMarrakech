# RUN MARRAKECH — Production Art Import Guide

The final art pass replaces bootstrap primitives with optimized original 3D assets.

## Required assets
- Player character: skinned humanoid, idle/run/jump/slide/hit animations.
- Marrakech architecture: riad facades, arches, doors, windows, souk stalls.
- Street props: lamps, signs, baskets, crates, carts, fountains.
- Vegetation: palms and small street plants.
- Traffic: original train/tram/car assets with simple collision meshes.
- Collectibles: coin and power-up models.
- VFX: dust, sparks, pickup burst, shield and speed trail.

## Mobile targets
- Prefer GLB/FBX with separate collision meshes.
- Use LODs for buildings and large props.
- Keep repeated props instanced.
- Atlas materials where practical.
- Avoid unnecessary transparent materials.
- Target 30/60 FPS on mid-range Android.
- Author high-resolution textures, then produce mobile-ready compressed variants.

## Art direction
Warm Marrakech ochre/pink plaster, sunlit streets, Moroccan geometric details, controlled teal/blue accents for gameplay readability, cinematic but performant lighting.

## Asset naming
RM_Player_*
RM_Building_*
RM_Prop_*
RM_Palm_*
RM_Traffic_*
RM_Coin
RM_PowerUp_*
RM_VFX_*

Do not use Subway Surfers copyrighted characters, models, textures, logos, sounds, or copied level assets.
