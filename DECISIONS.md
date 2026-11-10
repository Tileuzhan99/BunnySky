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
