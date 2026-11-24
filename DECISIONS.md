# Bunny Sky — development decisions

## Week 9 · Mobile UI (Assignment 8–9)

- 2026-11-03 Added a SafeArea panel to every screen and set the Canvas Scaler to 1920×1080, Match 1 (landscape).
- 2026-11-03 Problem: on a 4:3 tablet the level title overlapped the hearts and the boss HP bar overlapped the FIRE button. Moved the hint and the HP bar under the top row and renamed "LEVEL 3 - BOSS" to "BOSS" so it fits.
- 2026-11-03 Replaced the "x 3" text with three heart icons, because hearts read faster on a small screen.
- 2026-11-03 Added the pause screen (Resume, Restart, Menu, Time.timeScale = 0). The PAUSED title blended with the hint under the dim layer, so I put the buttons on a dark box.

## Week 10 · Enemy AI (Assignment 10)

- 2026-11-11 Added the AI Navigation package and a SmartEnemy base class: sight check (distance, angle, Raycast), on-screen check and a red tint + warning sound for wind-ups.
- 2026-11-11 Added three enemies with their own state machines: Patroller (crab) Patrol → Chase → Attack, Shooter (bee) Watch → Aim → Cooldown, Charger (skull) Wander → WindUp → Charge → Tired.
- 2026-11-11 Problem: the old Enemy class hurts the player on touch — an attack with no wind-up. New enemies use touchDamage = false and hurt only after a visible wind-up (crab 0.5 s, bee 0.6 s laser, skull 0.7 s).
- 2026-11-11 Charger must not dash from off-screen: it starts the wind-up only if the player is in range, visible by Raycast and the skull is on screen. Tested with the camera turned away: it keeps wandering.
- 2026-11-11 Built Level3, an arena with all three types together and crate walls as cover, so hiding from the bee's laser is a real choice. A tree at the start blocked the camera, so I moved it.
- 2026-11-11 Playtest 1 on a phone, two friends: after getting used to the controls they beat Level 3 on the 1st try — too easy. Some deaths came from missed jumps, not from enemies.
- 2026-11-11 Playtest 2 on a phone, two new testers: 2 tries each. Both deaths came from the Shooter's stinger; the Patroller and the Charger never killed anyone.
- 2026-11-11 Tuning after the playtests: Patroller chaseSpeed 3.5 → 4.2 and Charger chargeSpeed 11 → 13, so the crab and the skull are real threats too; Shooter windUp 0.6 → 0.7 s, so the laser is easier to react to and the bee is not the only killer.

## Week 11 · Data and saves (Assignment 11)

- 2026-11-17 Moved every balance number into ScriptableObjects in Assets/Data: PlayerStats, ItemValues, ProjectileData and enemy stats. The stats classes follow the enemy classes (EnemyStats → SmartEnemyStats → PatrollerStats …), so each enemy reads only its own fields. Field defaults were removed from the scripts.
- 2026-11-17 Replaced the three level scenes with three LevelData assets and one Game scene. A layout prefab keeps the islands; coins, hearts, hazards, enemies with waypoints, goal, start, camera, music and time limit are data. A new level is now a new asset, not new code.
- 2026-11-17 Save: JsonUtility to save.json in persistentDataPath (no BinaryFormatter). Loading is wrapped in try/catch: a missing, empty or broken file gives default progress and a message in the menu; out-of-range values are clamped by SaveData.Fix().
- 2026-11-17 Saving moments: after each level, on every settings change, in OnApplicationPause(true) (Android does not always call OnApplicationQuit) and on quit.
- 2026-11-17 Added level select (locked levels from the save), music/sound settings and reset progress with a confirm panel. Reset keeps the volume settings, because they are not progress. Labels on "RESET PROGRESS" and "YES, RESET" were wider than the buttons on a 4:3 tablet, so I made the font smaller.

## Week 12 · Sound pass (Assignment 12)

- 2026-11-25 Added an AudioMixer with Master, Music and SFX. Problem: a volume set from code through an exposed parameter is no longer controlled by snapshots, so the Paused snapshot could not lower the music. Fix: a child group Soundtrack under Music — the slider controls Music, the snapshot controls Soundtrack (-10 dB, lowpass 700 Hz).
- 2026-11-25 One SoundManager for the whole game (DontDestroyOnLoad). It keeps the music playing between scenes, so restarting a level does not restart the track, and the lose sound is not cut off by the scene reload.
- 2026-11-25 Music switches with a 1.5 s crossfade between two AudioSources; the fade uses unscaled time, and the mixer update mode is unscaled, so both work while the game is paused.
- 2026-11-25 All SFX go through SoundManager.Play with a random pitch 0.92–1.08, so repeated jumps, shots and coins don't sound the same. Enemy sounds are in their data assets; the wind-up sound plays at the start of the wind-up, so you hear an attack before it lands.
- 2026-11-25 Sliders use decibels (Log10 × 20). A slider at 0 would be Log10(0) = -infinity, so the slider minimum is 0.0001 (-80 dB, silent).
