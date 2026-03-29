using UnityEngine;

public class Platform_Spawn : MonoBehaviour
{
    public GameObject[] obsatacles;
    public float obstacleRatio = 0.3f;
    private bool isStepped = false; // Flag to track if the player has stepped on the platform    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log($"[Platform_Spawn] OnEnable - {gameObject.name}");

        foreach(var obstacle in obsatacles)
        {
            if (obstacle != null)
                obstacle.SetActive(Random.value < obstacleRatio);
        }
        isStepped = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !isStepped)
        {
            isStepped = true;
            Debug.Log($"[Platform_Spawn] Player stepped on {gameObject.name}");
            GameManager.instance.AddScore(1);
        }
    }
}
