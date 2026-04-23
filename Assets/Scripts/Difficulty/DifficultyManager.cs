using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    public float PlayerMaxSpeed { get; private set; }

    public float AISpeedMin { get; private set; }

    public float AISpeedMax { get; private set; }

    public float SpawnInterval { get; private set; }

    public float SpawnAheadDistance { get; private set; }

    public float DifficultyT { get; private set; }

    [Header("Player Max Speed (units/s)")]
    [Tooltip("Top speed the player can reach at the start of the run")]
    [SerializeField] float playerMaxSpeedEasy = 18f;
    [Tooltip("Top speed the player can reach at maximum difficulty")]
    [SerializeField] float playerMaxSpeedHard = 30f;

    [Header("AI Speed (units/s)")]
    [SerializeField] float aiSpeedMinEasy =  8f;
    [SerializeField] float aiSpeedMinHard = 16f;
    [SerializeField] float aiSpeedMaxEasy = 13f;
    [SerializeField] float aiSpeedMaxHard = 24f;

    [Header("Spawn Interval (seconds)")]
    [SerializeField] float spawnIntervalEasy = 1.4f;
    [SerializeField] float spawnIntervalHard = 0.6f;

    [Header("Spawn Ahead Distance (units)")]
    [SerializeField] float spawnAheadEasy = 70f;
    [SerializeField] float spawnAheadHard = 45f;

    [Header("Progression")]
    [Tooltip("Distance at which difficulty reaches its maximum")]
    [SerializeField] float maxDifficultyDistance = 3000f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        UpdateDifficulty(0f);
    }

    public void UpdateDifficulty(float distanceTraveled)
    {
        // Sqrt curve: fast progression early, gradually saturates toward the end
        float rawT = distanceTraveled / maxDifficultyDistance;
        DifficultyT = Mathf.Clamp01(Mathf.Sqrt(rawT));

        PlayerMaxSpeed     = Mathf.Lerp(playerMaxSpeedEasy,  playerMaxSpeedHard,  DifficultyT);
        AISpeedMin         = Mathf.Lerp(aiSpeedMinEasy,      aiSpeedMinHard,      DifficultyT);
        AISpeedMax         = Mathf.Lerp(aiSpeedMaxEasy,      aiSpeedMaxHard,      DifficultyT);
        SpawnInterval      = Mathf.Lerp(spawnIntervalEasy,   spawnIntervalHard,   DifficultyT);
        SpawnAheadDistance = Mathf.Lerp(spawnAheadEasy,      spawnAheadHard,      DifficultyT);
    }
}
