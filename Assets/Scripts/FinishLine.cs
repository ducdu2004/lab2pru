using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // QUAN TRỌNG: Phải có dòng này để dùng TextMeshPro

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 1f;
    [SerializeField] private ParticleSystem finishEffect;
    [SerializeField] private AudioClip finishSound;
    [SerializeField] private GameObject winPanel;

    // Thêm dòng này để gán cái chữ "FinalScoreText" vào
    [SerializeField] private TextMeshProUGUI finalScoreText;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            finishEffect.Play();
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null && finishSound != null)
            {
                audioSource.PlayOneShot(finishSound);
            }
            Invoke(nameof(ShowWinScreen), reloadDelay);
        }
    }

    private void ShowWinScreen()
    {
        if (winPanel != null)
        {
            // Lấy điểm từ ScoreManager và hiển thị lên bảng Win
            if (finalScoreText != null && ScoreManager.instance != null)
            {
                finalScoreText.text = "Tổng điểm: " + Mathf.FloorToInt(ScoreManager.instance.GetCurrentScore()).ToString();
            }

            winPanel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}