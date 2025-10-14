using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Win Condition")]
    public int targetBags = 12;
    private int collectedBags = 0;

    [Header("UI References")]
    [SerializeField] private TMP_Text bagCounterText; // assign in Inspector
    [SerializeField] private GameObject winPanel;     // inactive at start
    [SerializeField] private GameObject losePanel;    // inactive at start

    [SerializeField] private GameObject nextLevelButton; // inactive at start

    [Header("Options")]
    [SerializeField] private bool persistent = true; // set true if you want this to persist across scenes

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        if (persistent) DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (winPanel) winPanel.SetActive(false);
        if (losePanel) losePanel.SetActive(false);

        Time.timeScale = 1f;   // ensure unpaused on play
        UpdateBagUI();
    }

    public void AddBag()
    {
        collectedBags = Mathf.Min(collectedBags + 1, targetBags);
        UpdateBagUI();
        if (collectedBags >= targetBags)
            Win();
    }

    void UpdateBagUI()
    {
        if (bagCounterText)
        {
            bagCounterText.text = $"Bags: {collectedBags}/{targetBags}";
            bagCounterText.enabled = true;
            var c = bagCounterText.color; c.a = 1f; bagCounterText.color = c;
        }
        else
        {
            Debug.LogWarning("[GameManager] bagCounterText not assigned in Inspector.");
        }
    }

    public void Win() => ShowPanel(winPanel);

    public void Lose() => ShowPanel(losePanel);

    void ShowPanel(GameObject panel)
    {
        if (panel) panel.SetActive(true);
        Time.timeScale = 0f; // pause so game action stops
    }

    // Hook these to UI buttons
    public void ReplayLevel()
    {
        Time.timeScale = 1f;
        var current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

}
