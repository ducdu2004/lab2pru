using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    private CircleCollider2D _playerHead;
    [SerializeField] private float reloadDelay = 0.5f;
    [SerializeField] private ParticleSystem crashEffect;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private bool hasCrashed;

    private void Start()
    {
        _playerHead = GetComponent<CircleCollider2D>();
        hasCrashed = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Kiểm tra va chạm phần đầu với đất
        if (!hasCrashed && col.gameObject.CompareTag("Ground") && _playerHead.IsTouching(col.collider))
        {
            hasCrashed = true;
            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSound);
            GetComponent<PlayerController>().DisableInput();

            // Gọi hàm xử lý mất mạng thay vì load lại scene
            Invoke(nameof(RespawnAtPosition), reloadDelay);
        }
    }

    private void RespawnAtPosition()
    {
        // Trừ mạng trong GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.LoseLife();
        }

        // Reset trạng thái nhân vật để chơi tiếp tại chỗ
        transform.rotation = Quaternion.identity; // Đứng thẳng lại

        // Nhấc nhẹ nhân vật lên để tránh kẹt vào đất
        transform.position += new Vector3(0, 2f, 0);

        // Reset vận tốc vật lý
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Cho phép điều khiển lại
        GetComponent<PlayerController>().EnableInput();
        hasCrashed = false;
    }
}