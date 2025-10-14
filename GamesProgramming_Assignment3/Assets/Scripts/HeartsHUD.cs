using UnityEngine;
using UnityEngine.UI;

public class HeartsHUD : MonoBehaviour
{
    [Header("Hearts (left → right order)")]
    public Image[] heartImages;      // e.g., Heart1 (left), Heart2 (mid), Heart3 (right)

    [Header("Sprites")]
    public Sprite heartFull;         // Heart-1.png
    public Sprite heartEmpty;        // Heart-2.png

    [Header("Player (optional to assign)")]
    public PlayerHealth player;      // Drag Player here OR it will auto-find

    // Track current binding so we can rebind if needed
    PlayerHealth _bound;

    void Awake()
    {
        TryBindPlayer();
    }

    void OnEnable()
    {
        TryBindPlayer();
        if (_bound != null) _bound.OnHealthChanged += UpdateHearts;
    }

    void OnDisable()
    {
        if (_bound != null) _bound.OnHealthChanged -= UpdateHearts;
    }

    void Update()
    {
        // If player reference got cleared (e.g., scene reload), try to rebind
        if (_bound == null)
            TryBindPlayer();
    }

    void TryBindPlayer()
    {
        // if a player was manually assigned in Inspector, prefer that
        if (player == null)
        {
#if UNITY_2023_1_OR_NEWER
            player = FindFirstObjectByType<PlayerHealth>();
#else
            player = FindObjectOfType<PlayerHealth>();
#endif
        }

        // If we found a player and it's different from the currently bound one
        if (player != null && player != _bound)
        {
            // Unsubscribe old
            if (_bound != null) _bound.OnHealthChanged -= UpdateHearts;

            // Bind new
            _bound = player;
            _bound.OnHealthChanged += UpdateHearts;

            // Immediate refresh
            UpdateHearts(_bound.CurrentHealth, _bound.maxHealth);
        }
    }

    // This fills hearts from left; when health decreases, the RIGHTMOST empties first
    public void UpdateHearts(int current, int max)
    {
        if (heartImages == null || heartImages.Length == 0) return;
        for (int i = 0; i < heartImages.Length; i++)
        {
            bool showSlot = i < max;
            if (heartImages[i] != null)
            {
                heartImages[i].enabled = showSlot;
                if (showSlot)
                {
                    bool filled = i < current;   // indices < current are full
                    heartImages[i].sprite = filled ? heartFull : heartEmpty;
                }
            }
        }
    }

    // Optional: allow other scripts to set player explicitly (e.g., on respawn)
    public void SetPlayer(PlayerHealth newPlayer)
    {
        player = newPlayer;
        TryBindPlayer();
    }
}
