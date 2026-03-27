using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles;
    private bool stepped = false; // Flag to track if the player has stepped on the platform
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
        stepped = false; // Reset the stepped flag when the platform is enabled
        for(int i = 0; i < obstacles.Length; i++)
        {
            if(Random.Range(0, 3) == 0) // Randomly decide whether to activate each obstacle
            {
                obstacles[i].SetActive(true); // Activate the obstacle
            }
            else
            {
                obstacles[i].SetActive(false);
            }
             
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !stepped)
        {
            stepped = true; // Set the stepped flag to true when the player steps on the platform
            GameManager.instance.AddScore(1); // Increase the score by 1
            // You can add any additional logic here that should happen when the player steps on the platform
        }
    }
}
