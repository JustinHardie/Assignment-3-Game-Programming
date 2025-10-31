using UnityEngine;
using TMPro;

public class DifficultyMenu : MonoBehaviour
{
    [Header("UI")]
    public TMP_Dropdown dropdown;     // drag your TMP_Dropdown here in Inspector
    public TMP_Text currentLabel;     // (optional) shows "Difficulty: Medium"

    void Awake()
    {
        // Safety: find the dropdown if not assigned
        if (!dropdown) dropdown = GetComponentInChildren<TMP_Dropdown>(true);
    }

    void OnEnable()
    {
        // Initialize dropdown from saved/current difficulty
        if (DifficultyManager.Instance)
        {
            int idx = (int)DifficultyManager.Instance.Current;
            dropdown.SetValueWithoutNotify(idx); // set UI without triggering callback
            UpdateLabel();
        }

        // Subscribe to change
        dropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    void OnDisable()
    {
        dropdown.onValueChanged.RemoveListener(OnDropdownChanged);
    }

    private void OnDropdownChanged(int index)
    {
        if (!DifficultyManager.Instance) return;

        // 0=Easy, 1=Medium, 2=Hard
        DifficultyManager.Instance.SetDifficulty((Difficulty)index);
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        if (currentLabel && DifficultyManager.Instance)
            currentLabel.text = "Difficulty: " + DifficultyManager.Instance.Current.ToString();
    }
}
