using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] int lives = 3;

    [Header("Game Over Settings")]
    [SerializeField] GameObject lostPanel;
    [SerializeField] TextMeshProUGUI finalScoreTextLost;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        UpdateLivesUI();
        if (lostPanel != null) lostPanel.SetActive(false); // Đảm bảo bảng thua ẩn lúc đầu
    }

    public void LoseLife()
    {
        lives--;
        UpdateLivesUI();

        if (lives <= 0)
        {
            ShowGameOver(); // Gọi hàm hiện bảng thua ngay tại script này
        }
        else
        {
            RespawnPlayer();
        }
    }

    // Đưa hàm ShowGameOver về GameManager
    private void ShowGameOver()
    {
        if (lostPanel != null)
        {
            // Lấy điểm từ ScoreManager và in ra
            if (finalScoreTextLost != null && ScoreManager.instance != null)
            {
                finalScoreTextLost.text = "Score: " + Mathf.FloorToInt(ScoreManager.instance.GetCurrentScore()).ToString();
            }

            lostPanel.SetActive(true); // Hiện bảng thua
            Time.timeScale = 0f; // Dừng hoàn toàn game để không bị trừ mạng tiếp
        }
    }

    // Hàm này sẽ gắn vào nút "Menu" ở màn hình thua để chơi lại
    public void RestartGame()
    {
        Time.timeScale = 1f; // Chạy lại thời gian bình thường
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Tải lại màn chơi
    }

    void RespawnPlayer()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.transform.rotation = Quaternion.identity;
            player.Invoke("EnableInput", 0.5f);

            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            player.transform.position += new Vector3(0, 2f, 0);
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }
}