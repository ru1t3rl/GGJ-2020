using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Experimental.Rendering.DebugUI;

public class PauseMenuScript : MonoBehaviour
{
    static bool paused;
    public GameObject PauseMenu;

    public static bool Paused { get => paused; }

    // Start is called before the first frame update
    void Awake()
    {
        paused = false;
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PauseMenu.SetActive(true);
        }
    }

    public void Resume()
    {
        Debug.Log("Game Resumed");
        paused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenu.SetActive(false);
    }

    public void Menu()
    {
        paused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Main Menu");
    }

    public void Restart()
    {
        paused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    //bool TogglePause()
    //{
    //    if (PauseMenu.activeSelf)
    //    {
    //        Time.timeScale = 0;
    //        return (true);
    //    }
    //    else if (!PauseMenu.activeSelf)
    //    {
    //        Time.timeScale = 1;
    //        return (false);
    //    }
    //    return false;
    //}
}
