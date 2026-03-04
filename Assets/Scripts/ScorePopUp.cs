using UnityEngine;
using TMPro;

public class ScorePopUp : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float fadeDuration = 1f;
    private TextMeshProUGUI textMesh;
    private float timer;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void Setup(string text)
    {
        textMesh.text = text;
        textMesh.alpha = 1f;
        timer = fadeDuration;
    }

    void Update()
    {
        // Bay lên trên
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);

        // Mờ dần theo thời gian
        timer -= Time.deltaTime;
        textMesh.alpha = timer / fadeDuration;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}