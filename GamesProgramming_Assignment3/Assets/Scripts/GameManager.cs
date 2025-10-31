using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // -------- Singleton --------
    public static GameManager Instance { get; private set; }

    [Header("Win Condition")]
    [Tooltip("Number of bags required to win this level.")]
    public int targetBags = 12;

    [Header("UI References (assign in Inspector)")]
    public TMP_Text bagCounterText;      // e.g., "Bags: 0/12"
    public GameObject winPanel;          // set inactive in the scene
    public GameObject losePanel;         // set inactive in the scene

    [Header("Audio (optional)")]
    public AudioSource audioSource;      // optional
    public AudioClip winSound;           // optional
    public AudioClip loseSound;          // optional
    public AudioSource musicPlayer;      // optional background music

    [Header("Options")]
    public bool persistent = false;      // set true only if you want this to persist across scenes
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource; // drag in the Inspector
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    private AudioSource musicPlayer;

    void Awake()

    // -------- State --------
    public int CollectedBags { get; private set; } = 0;
    public bool IsGameOver { get; private set; } = false;

    // -------- Lifecycle --------
    private void Awake()


    // -------- State --------
    public int CollectedBags { get; private set; } = 0;
    public bool IsGameOver { get; private set; } = false;

    // -------- Lifecycle --------
    private void Awake()

    {
        // Basic singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (persistent)
            DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Ensure panels start hidden
        if (winPanel) winPanel.SetActive(false);
        if (losePanel) losePanel.SetActive(false);

        // Reset run state
        Time.timeScale = 1f;
        IsGameOver = false;
        CollectedBags = Mathf.Clamp(CollectedBags, 0, targetBags);

        UpdateBagUI();
        
        GameObject musicObject = GameObject.Find("MusicPlayer");
        if (musicObject)
            musicPlayer = musicObject.GetComponent<AudioSource>();
    }

    // -------- Public API --------
    public void AddBag()
    {
        if (IsGameOver) return;
    public void Win()
    {
        StopMusic();
        PlaySound(winSound);
        ShowPanel(winPanel);
    }

    public void Lose()
    {
        StopMusic();
        PlaySound(loseSound);
        ShowPanel(losePanel);
    }

    void StopMusic()
    {
        if (musicPlayer && musicPlayer.isPlaying)
            musicPlayer.Stop();
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource && clip)
            audioSource.PlayOneShot(clip, 0.35f);
    }

    void ShowPanel(GameObject panel)
    {
        if (panel) panel.SetActive(true);
        Time.timeScale = 0f; // pause so game action stops
    }

    // Hook these to UI buttons

        CollectedBags = Mathf.Min(CollectedBags + 1, targetBags);
        UpdateBagUI();

        if (CollectedBags >= targetBags)
            Win();
    }

    public void Win()
    {
        if (IsGameOver) return;
        IsGameOver = true;

        StopMusicIfAny();
        PlayOneShotIfAny(winSound);
        ShowPanelAndPause(winPanel);
        Debug.Log("[GameManager] WIN");
    }

    public void Lose()
    {
        if (IsGameOver) return;
        IsGameOver = true;

        StopMusicIfAny();
        PlayOneShotIfAny(loseSound);
        ShowPanelAndPause(losePanel);
        Debug.Log("[GameManager] LOSE");
    }

    public void ReplayLevel()
    {
        Time.timeScale = 1f;
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("3Dmenu");
    }

    // Optional helper if you change targets at runtime
    public void SetTargetBags(int value)
    {
        targetBags = Mathf.Max(0, value);
        CollectedBags = Mathf.Clamp(CollectedBags, 0, targetBags);
        UpdateBagUI();
    }

    // -------- Internals --------
    private void UpdateBagUI()
    {
        if (bagCounterText)
            bagCounterText.text = $"Bags: {CollectedBags}/{targetBags}";
    }

    private void ShowPanelAndPause(GameObject panel)
    {
        if (panel) panel.SetActive(true);
        Time.timeScale = 0f; // pause gameplay
    }

    private void StopMusicIfAny()
    {
        if (musicPlayer && musicPlayer.isPlaying)
            musicPlayer.Stop();
    }

    private void PlayOneShotIfAny(AudioClip clip)
    {
        if (audioSource && clip)
            audioSource.PlayOneShot(clip, 0.35f);
    }
}
