using UnityEngine;

[CreateAssetMenu(menuName = "Bunny Sky/Level Data")]
public class LevelData : ScriptableObject
{
    public string title;
    public string hint;
    public GameObject layout;
    public AudioClip music;
    public Vector3 playerStart;
    public Vector3 cameraOffset;
    public float timeLimit;
    public int coinsToOpenGoal;
    public Vector3 goalPosition;
    public float goalRotation;
    public Vector3[] coins;
    public Vector3[] hearts;
    public Placement[] hazards;
    public EnemySpawn[] enemies;
}

[System.Serializable]
public class Placement
{
    public GameObject prefab;
    public Vector3 position;
    public float rotation;
}

[System.Serializable]
public class EnemySpawn
{
    public GameObject prefab;
    public Vector3 position;
    public float rotation;
    public Vector3[] waypoints;
}
