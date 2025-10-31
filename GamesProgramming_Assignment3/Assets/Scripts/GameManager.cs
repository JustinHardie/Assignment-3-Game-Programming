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
    [SerializeField] private AudioSource audioSource; 
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    private AudioSource musicPlayer;

    [Header("Options")]
    public bool persistent = false;      

    // -------- State --------
    public int CollectedBags { get; private set; } = 0;
    public bool IsGameOver { get; private set; } = false;

    // -------- Lifecycle --------
    private void Awake()
    {
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
        if (winPanel) winPanel.SetActive(false);
        if (losePanel) losePanel.SetActive(false);

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
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("3Dmenu");
    }

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
        else
            Debug.LogWarning("[GameManager] bagCounterText not assigned!");
    }

    private void ShowPanelAndPause(GameObject panel)
    {
        if (panel) panel.SetActive(true);
        Time.timeScale = 0f;
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
