using UnityEngine;

public class PlatformLevel : MonoBehaviour
{
    public GameObject[] platforms;
    public int level;
    public float spawnTime;
    //public GameManager gameManager;

    private void Start()
    {
         

    }
    void Update()
    {
        if (GameManager.instance.isGameOver) return;
        spawnTime += Time.deltaTime;
        level = Random.Range(0, platforms.Length);
        if(spawnTime>=2.9f)
        {
            spawnTime = 0f;
            Instantiate(platforms[level].transform,new Vector2(20, -1.5f), Quaternion.identity);
        }
    }
}
