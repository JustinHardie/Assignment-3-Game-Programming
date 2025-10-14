using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorScript : MonoBehaviour
{
    void Start()
    {

    }

    public void OpenScene()
    {
        SceneManager.LoadScene("HaydenLevel");
    }
}
