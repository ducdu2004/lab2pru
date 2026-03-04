using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float torqueAmount = 15f;
    [SerializeField] private float boostSpeed = 45f;
    [SerializeField] private float normalSpeed = 25f;
    [SerializeField] private float dashForce = 20f;
    [SerializeField] bool canPlay;
    [SerializeField] AudioClip coinSound;
    [SerializeField] private int flipBonusScore = 50; // KHAI BÁO BIẾN Ở ĐÂY
    [SerializeField] private GameObject scorePopUpPrefab; // Bạn cần tạo 1 Prefab TMP_Text
    [SerializeField] private Transform canvasTransform;  // Kéo cái Canvas vào đây
    // --- BIẾN MỚI ---
    [SerializeField] bool canDash = true;
    private Rigidbody2D _rb2D;
    private SurfaceEffector2D _surfaceEffector2D;

    // --- BIẾN PHỤC VỤ QUAY 360 ---
    [SerializeField] private float autoRotateSpeed = 600f; // Tốc độ quay tự động
    private bool isAutoRotating = false;
    private float targetRotation = 0f;

    private void Start()
    {
        canPlay = true;
        _rb2D = GetComponent<Rigidbody2D>();
        _surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    private void Update()
    {
        if (canPlay)
        {
            // Nếu đang trong trạng thái quay tự động (Q/W) thì xử lý riêng
            if (isAutoRotating)
            {
                ApplyAutoRotation();
            }
            else
            {
                RotatePlayer(); // Phím mũi tên Trái/Phải
                HandleAutoRotateInput(); // Kiểm tra phím Q/W
            }

            RespondToBoost(); // Phím mũi tên Lên
            RespondToDash(); // Phím Space
        }
    }

    public void DisableInput()
    {
        canPlay = false;
    }

    private void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            _surfaceEffector2D.speed = normalSpeed;
        }
    }

    private void RespondToDash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            _rb2D.AddForce(transform.right * dashForce, ForceMode2D.Impulse);
            canDash = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canDash = true;
            // Nếu chạm đất thì dừng việc quay tự động để tránh bị kẹt góc
            isAutoRotating = false;
        }
    }

    private void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rb2D.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _rb2D.AddTorque(-torqueAmount);
        }
    }

    // Kiểm tra đầu vào cho việc quay 1 vòng
    private void HandleAutoRotateInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Quay theo chiều kim đồng hồ (giảm góc)
            targetRotation = _rb2D.rotation - 360f;
            isAutoRotating = true;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            // Quay ngược chiều kim đồng hồ (tăng góc)
            targetRotation = _rb2D.rotation + 360f;
            isAutoRotating = true;
        }
    }

    // Thực hiện việc xoay mượt mà cho đến khi đủ 360 độ
    //private void ApplyAutoRotation()
    //{
    //    float currentRotation = _rb2D.rotation;
    //    float nextRotation = Mathf.MoveTowards(currentRotation, targetRotation, autoRotateSpeed * Time.deltaTime);

    //    _rb2D.MoveRotation(nextRotation);

    //    // Kiểm tra nếu đã gần đạt đến góc đích thì kết thúc
    //    if (Mathf.Abs(nextRotation - targetRotation) < 0.1f)
    //    {
    //        _rb2D.rotation = targetRotation; // Đưa về góc chính xác
    //        isAutoRotating = false;
    //    }
    //}
    private void ApplyAutoRotation()
    {
        float currentRotation = _rb2D.rotation;
        float nextRotation = Mathf.MoveTowards(currentRotation, targetRotation, autoRotateSpeed * Time.deltaTime);

        _rb2D.MoveRotation(nextRotation);

        if (Mathf.Abs(nextRotation - targetRotation) < 0.1f)
        {
            _rb2D.rotation = targetRotation;
            isAutoRotating = false;

            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(flipBonusScore); //

                // Gọi hàm hiển thị text
                ShowFlipScore("+50");
            }
        }
    }

    private void ShowFlipScore(string message)
    {
        if (scorePopUpPrefab != null && canvasTransform != null)
        {
            // Tạo object từ Prefab và đặt vào Canvas
            GameObject textObj = Instantiate(scorePopUpPrefab, canvasTransform);

            // Đặt vị trí xuất hiện ở giữa màn hình
            RectTransform rect = textObj.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0, 50); // Hiện hơi cao lên một chút so với tâm

            // Gán nội dung text
            TextMeshProUGUI tmp = textObj.GetComponent<TextMeshProUGUI>();
            tmp.text = message;

            // Tự hủy sau 1 giây để tránh tràn bộ nhớ
            Destroy(textObj, 1f);
        }
    }

    public void EnableInput()
    {
        canPlay = true;
    }
}