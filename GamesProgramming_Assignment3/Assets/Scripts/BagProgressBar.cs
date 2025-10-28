using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BagProgressBar : MonoBehaviour
{
    [Header("UI References")]
    public Slider bagSlider;
    public TMP_Text bagText;

    [Header("Goal Settings")]
    public int requiredBags = 12;

    private int currentBags = 0;

    void Start()
    {
        // Reset counter each time the level loads
        currentBags = 0;

        // Slider setup
        bagSlider.maxValue = requiredBags;
        bagSlider.value = currentBags;
        UpdateText();

        // Optional: listen for scene reloads if needed
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset UI again in case this persists across reloads
        currentBags = 0;
        bagSlider.value = 0;
        UpdateText();
    }

    public void AddBag()
    {
        currentBags++;
        bagSlider.value = Mathf.Min(currentBags, requiredBags);
        UpdateText();
    }

    private void UpdateText()
    {
        if (bagText != null)
        {
            bagText.text = $"Bags: {currentBags} / {requiredBags}";
        }
    }
}
