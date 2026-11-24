using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public static int levelIndex;
    public static LevelLoader current;

    public LevelData[] levels;
    public Player player;
    public CameraFollow cameraFollow;
    public GameObject coinPrefab;
    public GameObject heartPrefab;
    public GameObject goalPrefab;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI hintText;
    public TextMeshProUGUI timerText;
    public GameObject pauseButton;
    public PauseMenu pauseMenu;
    public GameObject completePanel;
    public TextMeshProUGUI completeText;

    LevelData level;
    float timeLeft;
    bool finished;

    void Awake()
    {
        current = this;
        level = levels[levelIndex];

        Instantiate(level.layout);
        foreach (Vector3 p in level.coins) Instantiate(coinPrefab, p, Quaternion.identity);
        foreach (Vector3 p in level.hearts) Instantiate(heartPrefab, p, Quaternion.identity);
        foreach (Placement h in level.hazards) Instantiate(h.prefab, h.position, Quaternion.Euler(0f, h.rotation, 0f));
        foreach (EnemySpawn e in level.enemies) SpawnEnemy(e);
        Instantiate(goalPrefab, level.goalPosition, Quaternion.Euler(0f, level.goalRotation, 0f));

        player.transform.position = level.playerStart;
        player.coinsNeeded = level.coinsToOpenGoal;
        cameraFollow.offset = level.cameraOffset;
        cameraFollow.transform.position = level.playerStart + level.cameraOffset;
        titleText.text = level.title;
        hintText.text = level.hint;

        timeLeft = level.timeLimit;
        completePanel.SetActive(false);
    }

    void Start()
    {
        SoundManager.instance.PlayMusic(level.music);
    }

    void SpawnEnemy(EnemySpawn spawn)
    {
        GameObject enemy = Instantiate(spawn.prefab, spawn.position, Quaternion.Euler(0f, spawn.rotation, 0f));

        Transform[] points = new Transform[spawn.waypoints.Length];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = new GameObject(spawn.prefab.name + " Point " + (i + 1)).transform;
            points[i].position = spawn.waypoints[i];
        }

        PatrolEnemy patrol = enemy.GetComponent<PatrolEnemy>();
        if (patrol != null)
        {
            patrol.pointA = points[0];
            patrol.pointB = points[1];
        }

        Patroller patroller = enemy.GetComponent<Patroller>();
        if (patroller != null)
        {
            patroller.waypoints = points;
        }
    }

    void Update()
    {
        if (finished) return;

        timeLeft -= Time.deltaTime;
        timerText.text = FormatTime(timeLeft);

        if (timeLeft <= 0f)
        {
            SoundManager.Play(SoundManager.instance.loseSound);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void CompleteLevel()
    {
        if (finished) return;
        finished = true;

        float time = level.timeLimit - timeLeft;
        float best = SaveSystem.CompleteLevel(levelIndex, time);

        SoundManager.Play(SoundManager.instance.winSound);
        pauseButton.SetActive(false);
        pauseMenu.enabled = false;
        completeText.text = "Time " + FormatTime(time) + "    Best " + FormatTime(best);
        completePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        if (levelIndex + 1 < levels.Length)
        {
            levelIndex++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            SceneManager.LoadScene("Boss");
        }
    }

    public static string FormatTime(float seconds)
    {
        int total = Mathf.CeilToInt(Mathf.Max(seconds, 0f));
        return total / 60 + ":" + (total % 60).ToString("00");
    }
}
