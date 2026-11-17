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
  Prefabs/     player, enemies, pickups, hazards, projectiles
  Scenes/      Menu, Game, Boss, Win
  Scripts/
    Combat/    Projectile, Bullet, Fireball
    Data/      ScriptableObject classes
    Enemies/   Enemy, PatrolEnemy, Boss, SmartEnemy, Patroller, Shooter, Charger
    Level/     LevelLoader, Coin, HeartPickup, Hazard, Portal
    Player/    Player, Health, CameraFollow
    Save/      SaveData, SaveSystem, SaveOnPause
    UI/        Menu, LevelSelect, SettingsMenu, PauseMenu, SafeArea, MusicVolume
```

Decisions are logged in [DECISIONS.md](DECISIONS.md). GDD: [Docs/BunnySky_GDD.pdf](Docs/BunnySky_GDD.pdf).

## Assets and licenses

- [Ultimate Platformer Pack](https://quaternius.itch.io/ultimate-platformer-pack) by Quaternius — CC0
- Sounds: own work (course assignments) and generated 8-bit effects
- Font: Liberation Sans (TextMeshPro default) — SIL Open Font License
- UI icons rendered from the Quaternius models; UI shapes generated (own work)
