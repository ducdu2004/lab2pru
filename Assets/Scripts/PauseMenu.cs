using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel; // Phải kéo PausePanel vào đây trong Inspector
    bool isPaused = false;

    void Update()
    {
        // Kiểm tra phím Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Chạy lại thời gian
        isPaused = false;
        Cursor.visible = false;
    }

    void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Dừng thời gian
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}