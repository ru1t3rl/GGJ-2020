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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
