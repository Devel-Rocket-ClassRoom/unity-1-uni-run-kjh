using UnityEngine;

public class WaningCresentJelly : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddScore(20000);
            gameObject.SetActive(false);
        }
    }
}
