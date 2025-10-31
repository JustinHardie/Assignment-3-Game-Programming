using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu3D : MonoBehaviour
{
    public void OnStartClick()
    {
        // Load your 2D level scene
        SceneManager.LoadScene("LevelSelectorScene");

    }

    public void LoadLevelByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
