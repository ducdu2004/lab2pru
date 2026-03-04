using UnityEngine;

public class SkatingScore : MonoBehaviour
{
    [SerializeField] float pointsPerSecond = 1f;
    bool isGrounded = false;

    void Update()
    {
        // Nếu đang chạm đất thì yêu cầu ScoreManager cộng điểm
        if (isGrounded && ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScoreOverTime(pointsPerSecond);
        }
    }

    // Kiểm tra khi chạm vào mặt đất
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Kiểm tra khi rời khỏi mặt đất (bay lên không trung)
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}