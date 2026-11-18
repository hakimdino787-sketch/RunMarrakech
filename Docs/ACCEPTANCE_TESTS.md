# RUN MARRAKECH — Unity acceptance checks

Run these in Unity Editor after generating `Assets/Scenes/RunMarrakech.unity`.

1. Launch/menu — Start screen is visible; Start Run hides it and the player begins moving forward.
2. Three lanes — Swipe left/right and arrow keys move between left, centre and right lanes without leaving the road.
3. Jump — Swipe up, Space or Up Arrow performs one jump; repeated input while airborne does not stack jumps.
4. Slide — Swipe down or Down Arrow lowers the CharacterController temporarily and restores its original dimensions.
5. Obstacle rules — Jump-over barriers can be cleared by jumping; overhead bars can be cleared by sliding; solid obstacles end the run.
6. Shield — Collect a shield and confirm it absorbs one otherwise-fatal collision.
7. Coins and magnet — Coins increase the counter and score; magnet and power-up timers activate and expire.
8. Pause/resume — Pause freezes gameplay; Resume restores it without resetting the run.
9. Game over/restart — Game-over panel shows score and coins; Restart returns to the start screen and preserves the high score.
10. World loop — The two 144-metre environment chunks recycle without a visible gap during a long run.
11. Android portrait — Test on a physical mid-range Android device; verify touch input, UI safe areas, loading, and sustained frame rate.
12. Release build — Build APK and AAB using the menu commands; install the APK and launch it outside the Editor.

Do not mark any item passed until it has been executed in the target environment.
