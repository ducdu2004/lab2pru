using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int scoreValue = 10;
    [SerializeField] AudioClip coinSound; // Kéo file âm thanh vào ô này trong Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (coinSound != null)
            {
                // Phát âm thanh tại vị trí hiện tại của đồng xu
                AudioSource.PlayClipAtPoint(coinSound, transform.position);
                Debug.Log("tinh tinh");
            }

            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(scoreValue);
            }

            Destroy(gameObject);
        }
    }
}