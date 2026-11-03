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
| 3 | Boss | Horned King, fireballs, HP bar |
| 4 | Win | You Win + Menu |

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
