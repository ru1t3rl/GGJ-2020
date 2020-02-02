using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private AudioClip MenuMusic;

    public void QuitGame()
    {
        Application.Quit();
    }

    void Start()
    {
        AudioManager.Instance.PlayMusic(MenuMusic);
        AudioManager.Instance.SetMusicVolume(0.05f);

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
