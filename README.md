# Bunny Sky

A blaster-wielding bunny hops across floating sky islands, collects coins and fights the monsters that took over the sky, ending each world with a boss.

- **Genre:** 3D action platformer, third-person
- **Platform:** Android, landscape
- **Engine:** Unity 6000.6.0f1, Built-in Render Pipeline, Input System, uGUI + TextMeshPro
- **Course:** Game Development Fundamentals, RK2 (weeks 9–13) · Kadirbek

## Scenes

| # | Scene | What is there |
|---|-------|---------------|
| 0 | Menu | title, Play, Quit |
| 1 | Level1 | tutorial: platforms, coins, spikes, heart, goal flag |
| 2 | Level2 | crabs, flying bee, saw, lift platform |
| 3 | Level3 | enemy arena: Patroller, Shooter and Charger together, crate cover |
| 4 | Boss | Horned King, fireballs, HP bar |
| 5 | Win | You Win + Menu |

## Enemies

| Enemy | Model | States | How to beat it |
|-------|-------|--------|----------------|
| Patroller (`Patroller.cs`) | crab | Patrol → Chase → Attack | it sees only in front (120°, 8 m, walls block); step back when it flashes red (0.5 s) |
| Shooter (`Shooter.cs`) | bee | Watch → Aim → Cooldown | red laser for 0.6 s, then a stinger; hide behind crates or sidestep |
| Charger (`Charger.cs`) | skull | Wander → WindUp → Charge → Tired | flashes red for 0.7 s, dashes straight; sidestep or jump, shoot it while it is grey (tired) |

All three inherit `SmartEnemy` (sight with Raycast, on-screen check, wind-up tint and sound), which inherits `Enemy` (HP, hits, death). Movement uses NavMeshSurface + NavMeshAgent. Tuning numbers are public fields on the prefabs. Decisions are logged in [DECISIONS.md](DECISIONS.md).

## Controls

| On-screen | Where | Keyboard | Action |
|-----------|-------|----------|--------|
| Joystick | bottom-left | WASD / arrows | move |
| JUMP | bottom-right | Space | jump |
| FIRE | bottom-right | X | shoot |
| II | top-right | Esc | pause |

## Mobile UI

- Canvas Scaler: Scale With Screen Size, 1920 × 1080, Match = 1 (height)
- All buttons and text live inside a `SafeArea` panel (`Assets/Scripts/SafeArea.cs`)
- Pause screen: Resume, Restart, Menu; `Time.timeScale = 0` while paused
- Tested in Device Simulator: 16:9 phone, notch phone (20:9), 4:3 tablet

## Documents

- [GDD (PDF)](Docs/BunnySky_GDD.pdf)

## Assets and licenses

- [Ultimate Platformer Pack](https://quaternius.itch.io/ultimate-platformer-pack) by Quaternius — CC0
- Sounds: own work (course assignments) and generated 8-bit effects
- Font: Liberation Sans (TextMeshPro default) — SIL Open Font License
- UI icons rendered from the Quaternius models; UI shapes generated (own work)
