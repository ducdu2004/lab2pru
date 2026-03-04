using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] TextMeshProUGUI scoreText;
    float currentScore = 0f; // Dùng float để cộng dồn chính xác theo thời gian
                             // Thêm hàm này vào trong class ScoreManager để các script khác lấy được điểm
    public float GetCurrentScore()
    {
        return currentScore;
    }
    void Awake()
    {
        if (instance == null) instance = this;
    }

    // Hàm cộng điểm tức thời (cho Coin)
    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateUI();
    }

    // Hàm cộng điểm theo thời gian (cho trượt tuyết)
    public void AddScoreOverTime(float pointsPerSecond)
    {
        currentScore += pointsPerSecond * Time.deltaTime;
        UpdateUI();
    }

    void UpdateUI()
    {
        // Làm tròn số điểm để hiển thị số nguyên
        scoreText.text = "Score: " + Mathf.FloorToInt(currentScore).ToString();
    }
}