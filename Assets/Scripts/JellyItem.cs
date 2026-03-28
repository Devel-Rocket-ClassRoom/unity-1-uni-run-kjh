using UnityEngine;

public class JellyItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddScore(1111);
            gameObject.SetActive(false);
        }
    }
}
