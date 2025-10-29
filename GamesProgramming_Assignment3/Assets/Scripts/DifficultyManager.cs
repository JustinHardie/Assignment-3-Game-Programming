using UnityEngine;

public enum Difficulty { Easy = 0, Medium = 1, Hard = 2 }

[System.Serializable]
public struct DifficultyConfig
{
    public float obstacleSpeedMul;   // affects spawners / movers
    public float damageMul;          // affects damage taken
    public float timeLimitMul;       // if you have timers (1 = default)
    public int bagGoalBonus;         // optional: extra bags required on harder modes
}

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    // Default configs (tweak to taste)
    public DifficultyConfig easy   = new DifficultyConfig {  damageMul = 0.75f};
    public DifficultyConfig medium = new DifficultyConfig {  damageMul = 1.00f};
    public DifficultyConfig hard   = new DifficultyConfig { damageMul = 1.25f };

    const string KEY = "GAME_DIFFICULTY";
    [SerializeField] Difficulty current = Difficulty.Medium;

    public Difficulty Current => current;
    public DifficultyConfig Config
    {
        get
        {
            switch (current)
            {
                case Difficulty.Easy:   return easy;
                case Difficulty.Hard:   return hard;
                default:                return medium;
            }
        }
    }

    void Awake()
    {
        if (Instance && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey(KEY))
            current = (Difficulty)PlayerPrefs.GetInt(KEY, (int)Difficulty.Medium);
        else
            PlayerPrefs.SetInt(KEY, (int)current);
    }

    public void SetDifficulty(Difficulty d)
    {
        current = d;
        PlayerPrefs.SetInt(KEY, (int)d);
        PlayerPrefs.Save();
        Debug.Log($"[Difficulty] Set to {d}");
    }
}
