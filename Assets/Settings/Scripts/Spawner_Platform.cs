using UnityEngine;

public class Spawner_Platform : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject platformPrefab;
    public int platformCount = 3;

    public  Vector2 spawnTimeRange = new Vector2(1.25f, 2.25f);
    private float timeSpawn;


    public Vector2 yRange = new Vector2(-3.5f, 1.5f);
    private float xPos = 10f;

    private GameObject[] platforms;
    private int currentIndex = 0;

    private float lastSpawnTime;  

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        platforms = new GameObject[platformCount];
        for (int i = 0; i < platformCount; i++)
        {
            platforms[i] = Instantiate(platformPrefab);
            platforms[i].SetActive(false);
        }
    }
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController")?.GetComponent<GameManager>();
        lastSpawnTime = Time.time;
        timeSpawn = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isGameOver) return;

        if(Time.time > lastSpawnTime + timeSpawn)
        {
            lastSpawnTime = Time.time;
            timeSpawn = Random.Range(spawnTimeRange.x, spawnTimeRange.y);

            Vector2 Pos;
            Pos.x = xPos;
            Pos.y = Random.Range(yRange.x, yRange.y);
            platforms[currentIndex].transform.position = Pos;

            //float y = Random.Range(yRange.x, yRange.y);
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            currentIndex++;

            currentIndex = (int)Mathf.Repeat(currentIndex, platformCount);
        }
        
    }
}
