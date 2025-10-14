using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Buttons")]
    public Button[] levelButtons; // Assign buttons in order (Level1, Level2, Level3)

    void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("LevelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool unlocked = (i + 1) <= unlockedLevel;
            levelButtons[i].interactable = unlocked;
        }
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("LevelReached");
        PlayerPrefs.Save();
        Debug.Log("Progress Reset!");
    }
}
