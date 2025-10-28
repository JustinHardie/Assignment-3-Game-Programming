using UnityEngine;

public class ItemBank : MonoBehaviour
{
    public static ItemBank Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        // Optional: uncomment to persist across scenes
        DontDestroyOnLoad(gameObject);

        SaveSystem.Load();
    }

    public int TotalItems => SaveSystem.Data.totalItems;

    public void AddItems(int amount)
    {
        if (amount <= 0) return;
        SaveSystem.Data.totalItems += amount;
        SaveSystem.Save();
        // Debug.Log($"[ItemBank] Total items: {SaveSystem.Data.totalItems}");
    }

    public void ResetAll()
    {
        SaveSystem.Reset();
    }
}
