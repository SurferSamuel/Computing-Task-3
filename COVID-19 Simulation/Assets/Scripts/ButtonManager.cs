using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void RestartGame()
    {
        // Reloads current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        // Quit the simulator
        Application.Quit();
    }

    public void PauseGame()
    {
        // Set time scale to 0 (paused)
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        // Set time scale to 1 (resumed)
        Time.timeScale = 1;
    }
}
