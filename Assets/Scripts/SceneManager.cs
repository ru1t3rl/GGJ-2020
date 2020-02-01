using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    void Start()
    {
        Cursor.visible = true;
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
    }
}
