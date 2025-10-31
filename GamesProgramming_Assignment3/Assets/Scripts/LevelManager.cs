using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Buttons in order (Level 1, 2, 3, 4)")]
    public Button[] levelButtons;

    [Header("Optional: requirement labels aligned with buttons")]
    public TMPro.TMP_Text[] requirementLabels; // e.g., "", "Need 12 items", "Need 24 items"

    // Gates
    private const int REQ_LEVEL_2 = 12;
    private const int REQ_LEVEL_3 = 24;

    private const int REQ_LEVEL_4 = 36;

    void Start()
    {
        // Ensure save is loaded
        SaveSystem.Load();

        int items = SaveSystem.Data.totalItems;

        // Level 1: always unlocked
        SetButtonState(0, true, "");

        // Level 2: needs 12 items
        bool l2 = items >= REQ_LEVEL_2;
        SetButtonState(1, l2, l2 ? "" : $"Requires {REQ_LEVEL_2} items");

        // Level 3: needs 24 items
        bool l3 = items >= REQ_LEVEL_3;
        SetButtonState(2, l3, l3 ? "" : $"Requires {REQ_LEVEL_3} items");

        // Level 4: needs 36 items
        bool l4 = items >= REQ_LEVEL_3;
        SetButtonState(3, l4, l4 ? "" : $"Requires {REQ_LEVEL_4} items");
    }

    private void SetButtonState(int index, bool interactable, string label)
    {
        if (levelButtons != null && index < levelButtons.Length && levelButtons[index] != null)
            levelButtons[index].interactable = interactable;

        if (requirementLabels != null && index < requirementLabels.Length && requirementLabels[index] != null)
        {
            requirementLabels[index].text = label;
            requirementLabels[index].gameObject.SetActive(!string.IsNullOrEmpty(label));
        }
    }

    // Wire these from the Button onClick (pass scene name or build index)
    public void LoadLevelByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLevelByIndex(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void ResetProgress()
    {
        SaveSystem.Reset();
        Debug.Log("Progress reset (JSON)");
        // Optional: refresh UI
        Start();
    }
}
