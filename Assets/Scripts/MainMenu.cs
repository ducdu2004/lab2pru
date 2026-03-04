using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject helpPanel;

    void Start()
    {
        Time.timeScale = 0f;
        if (menuCanvas != null) menuCanvas.SetActive(true);
        if (helpPanel != null) helpPanel.SetActive(false);
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        if (menuCanvas != null) menuCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenHelp()
    {
        if (helpPanel != null)
        {
            helpPanel.SetActive(true);
            if (menuCanvas != null) menuCanvas.SetActive(false);
        }
    }

    public void CloseHelp()
    {
        if (helpPanel != null)
        {
            helpPanel.SetActive(false);
            if (menuCanvas != null) menuCanvas.SetActive(true);
        }
    }
   
        public void MenuStart()
    {
        // Load lại Scene số 0 (hoặc Scene hiện tại) để làm mới toàn bộ dữ liệu
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

}