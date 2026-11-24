# Bunny Sky

A blaster-wielding bunny hops across floating sky islands, collects coins and fights the monsters that took over the sky, ending each world with a boss.

- **Genre:** 3D action platformer, third-person
- **Platform:** Android, landscape
- **Engine:** Unity 6000.6.0f1, Built-in Render Pipeline, Input System, AI Navigation, uGUI + TextMeshPro
- **Course:** Game Development Fundamentals, RK2 (weeks 9–13) · Kadirbek

## Scenes

| # | Scene | What is there |
|---|-------|---------------|
| 0 | Menu | title, Play → level select, Settings, Quit |
| 1 | Game | loads Level 1, 2 or 3 from a `LevelData` asset |
| 2 | Boss | Horned King, fireballs, HP bar (opens after level 3) |
| 3 | Win | You Win + Menu |

## Levels from data

`Assets/Data/Levels/Level1..3.asset` (`LevelData`) hold everything about a level: layout prefab (`Assets/Levels/`), music, player start, camera offset, time limit, coins needed, goal, coins, hearts, hazards and enemy spawns with waypoints. `LevelLoader` in the Game scene spawns the selected one.

| Level | Content | Time |
|-------|---------|------|
| 1 | tutorial: platforms, 5 coins, spikes, heart | 2:00 |
| 2 | crabs, bee, saw, lift, 7 coins | 3:00 |
| 3 | arena with Patroller, Shooter and Charger, crate cover, 6 coins | 3:00 |

## Balance data

All balance numbers live in ScriptableObjects in `Assets/Data` — no numbers in code:

| Asset | Class | Used by |
|-------|-------|---------|
| `Player/PlayerStats` | `PlayerStats` | `Player`, `Health` |
| `Items/ItemValues` | `ItemValues` | `Coin`, `HeartPickup`, `Hazard` |
| `Projectiles/Bullet, Fireball, Stinger` | `ProjectileData` | `Projectile` → `Bullet`, `Fireball` |
| `Enemies/Crab, Bee` | `PatrolEnemyStats` | `PatrolEnemy` |
| `Enemies/Boss` | `BossStats` | `Boss` |
| `Enemies/Patroller, Shooter, Charger` | `PatrollerStats`, `ShooterStats`, `ChargerStats` (all `SmartEnemyStats`) | `Patroller`, `Shooter`, `Charger` |

## Saves

`SaveSystem` writes `save.json` to `Application.persistentDataPath` with `JsonUtility`:

```json
{
    "unlockedLevels": 2,
    "bestTimes": [ 41.8, 0.0, 0.0 ],
    "bossBeaten": false,
    "musicVolume": 1.0,
    "soundVolume": 1.0
}
```

- Saved after every level, when a setting changes, in `OnApplicationPause(true)` and on quit.
- A missing, empty or broken file never crashes the game: it loads defaults and the menu shows a message. Out-of-range values are clamped.
- Reset progress (with a confirm panel) keeps the settings.

## Sound

`Assets/Audio/MainMixer.mixer`:

```
Master
├── Music        (exposed: MusicVolume, Settings slider)
│   └── Soundtrack   (Paused snapshot: -10 dB + Lowpass 700 Hz)
└── SFX          (exposed: SFXVolume, Settings slider)
```

- `SoundManager` (one per game, survives scene loads): two music sources with a 1.5 s crossfade between menu, level and boss tracks; the same track is not restarted when a level reloads. `SoundManager.Play(clip)` plays every SFX with a random pitch 0.92–1.08.
- Volume sliders work in decibels (`Log10(value) × 20`) and are saved in `save.json`.
- Pause switches to the **Paused** snapshot (quieter, muffled music), Resume back to **Normal**.
- Enemy sounds live in the enemy data assets (`windUpSound`, `attackSound`, `hitSound`, `deathSound`). The wind-up sound starts with the wind-up, before the attack lands.

| Event | Sound |
|-------|-------|
| Jump | `jump.wav` |
| Shoot | `shoot.wav` |
| Coin / heart pickup | `coin.wav` / `heal.wav` |
| Player hit | `hurt.wav` |
| Enemy wind-up | `warn.wav` |
| Enemy attack | `bite.wav` (crab), `sting.wav` (bee), `dash.wav` (skull, boss fireballs) |
| Enemy hit / boss hit | `enemy_hit.wav` / `bosshit.wav` |
| Enemy death | `enemy_die.wav` |
| UI click | `click.wav` |
| Level complete / Win screen | `complete.wav` / `win.wav` |
| Lose (death, fall, time up) | `lose.wav` |
| Music: menu / levels / boss | `menu_music.wav` / `music.wav` / `bossmusic.wav` |

## Enemies

| Enemy | Model | States | How to beat it |
|-------|-------|--------|----------------|
| Patroller | crab | Patrol → Chase → Attack | sees only in front (120°, 8 m, walls block); step back when it flashes red |
| Shooter | bee | Watch → Aim → Cooldown | red laser, then a stinger; hide behind crates or sidestep |
| Charger | skull | Wander → WindUp → Charge → Tired | flashes red, dashes straight; sidestep or jump, shoot it while it is grey |

## Controls

| On-screen | Where | Keyboard | Action |
|-----------|-------|----------|--------|
| Joystick | bottom-left | WASD / arrows | move |
| JUMP | bottom-right | Space | jump |
| FIRE | bottom-right | X | shoot |
| II | top-right | Esc | pause |

## Project structure

```
Assets/
  Data/        ScriptableObject assets (player, items, projectiles, enemies, levels)
  Levels/      level layout prefabs + Level 3 NavMesh
  Audio/       sounds, music, MainMixer
  Prefabs/     player, enemies, pickups, hazards, projectiles, SoundManager
  Scenes/      Menu, Game, Boss, Win
  Scripts/
    Combat/    Projectile, Bullet, Fireball
    Data/      ScriptableObject classes
    Enemies/   Enemy, PatrolEnemy, Boss, SmartEnemy, Patroller, Shooter, Charger
    Level/     LevelLoader, Coin, HeartPickup, Hazard, Portal
    Player/    Player, Health, CameraFollow
    Save/      SaveData, SaveSystem, SaveOnPause
    UI/        Menu, LevelSelect, SettingsMenu, PauseMenu, SafeArea
    Audio/     SoundManager, ClickSound
```

Decisions are logged in [DECISIONS.md](DECISIONS.md). GDD: [Docs/BunnySky_GDD.pdf](Docs/BunnySky_GDD.pdf).

## Assets and licenses

- [Ultimate Platformer Pack](https://quaternius.itch.io/ultimate-platformer-pack) by Quaternius — CC0

### Sounds and music

| File | Source | License |
|------|--------|---------|
| `hurt.wav`, `heal.wav`, `shoot.wav`, `win.wav` | own work, made for course assignment A6 | own work, CC0 1.0 |
| `jump.wav`, `coin.wav`, `bosshit.wav`, `music.wav`, `bossmusic.wav` | generated 8-bit chiptune with a Python synth script (midterm) | own work, CC0 1.0 |
| `warn.wav` | generated with a Python synth script (Assignment 10) | own work, CC0 1.0 |
| `click.wav`, `enemy_hit.wav`, `enemy_die.wav`, `bite.wav`, `sting.wav`, `dash.wav`, `complete.wav`, `lose.wav`, `menu_music.wav` | generated with a Python synth script (Assignment 12) | own work, CC0 1.0 |

No third-party audio is used.

### Other
- Font: Liberation Sans (TextMeshPro default) — SIL Open Font License
- UI icons rendered from the Quaternius models; UI shapes generated (own work)
