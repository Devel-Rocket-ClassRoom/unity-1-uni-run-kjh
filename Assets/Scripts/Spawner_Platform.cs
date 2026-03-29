 

using UnityEngine;

public class Spawner_Platform : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject[] platformPrefab;  // 3개 할당
    public int platformCount = 3;

    public Vector2 spawnTimeRange = new Vector2(7.5f, 7.5f);
    private float timeSpawn;

    public float yPos = -1.5f;
    private float xPos = 10f;

    private GameObject[] platforms;     // 1차원으로 단순화
    private int currentIndex;

    private float lastSpawnTime;

    private void Awake()
    {
        if (platformPrefab == null || platformPrefab.Length == 0)
        {
            Debug.LogError("platformPrefab이 비어있습니다.");
            return;
        }

        // 프리팹 종류 수만큼 미리 생성
        platforms = new GameObject[platformPrefab.Length];

        for (int i = 0; i < platformPrefab.Length; i++)
        {
            platforms[i] = Instantiate(platformPrefab[i]);
            platforms[i].SetActive(false);
        }
    }

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController")?.GetComponent<GameManager>();
        lastSpawnTime = Time.time;
        timeSpawn = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
    }

    private void Update()
    {
        if (GameManager.instance.isGameOver) return;

        if (Time.time > lastSpawnTime + timeSpawn)
        {
            lastSpawnTime = Time.time;
            timeSpawn = Random.Range(spawnTimeRange.x, spawnTimeRange.y);

            // 랜덤으로 하나 선택
            currentIndex = Random.Range(0, platforms.Length);

            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);
        }
    }
}


    //private GameManager gameManager;

    //public GameObject platformPrefab;
    //public int platformCount = 3;

    //public  Vector2 spawnTimeRange = new Vector2(1.25f, 2.25f);
    //private float timeSpawn;


    //public Vector2 yRange = new Vector2(-3.5f, 1.5f);
    //private float xPos = 10f;

    //private GameObject[] platforms;
    //private int currentIndex = 0;

    //private float lastSpawnTime;  

    //// Start is called once before the first execution of Update after the MonoBehaviour is created

    //private void Awake()
    //{
    //    Debug.Log($"[Spawner_Platform] Awake - platformCount: {platformCount}");
    //    platforms = new GameObject[platformCount];
    //    for (int i = 0; i < platformCount; i++)
    //    {
    //        platforms[i] = Instantiate(platformPrefab);

    //        // Object_Scrolling이 없으면 추가 (플랫폼이 왼쪽으로 이동해야 화면에 보임)
    //        if (platforms[i].GetComponent<Object_Scrolling>() == null)
    //        {
    //            Debug.LogWarning($"[Spawner_Platform] platform[{i}]에 Object_Scrolling이 없어서 추가합니다. 프리팹에 직접 추가하는 것을 권장합니다.");
    //            platforms[i].AddComponent<Object_Scrolling>();
    //        }

    //        platforms[i].SetActive(false);
    //    }
    //}
    //void Start()
    //{
    //    gameManager = GameObject.FindWithTag("GameController")?.GetComponent<GameManager>();
    //    lastSpawnTime = Time.time;
    //    timeSpawn = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
    //    Debug.Log($"[Spawner_Platform] Start - timeSpawn: {timeSpawn}");
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if(GameManager.instance.isGameOver) return;

    //    if(Time.time > lastSpawnTime + timeSpawn)
    //    {
    //        lastSpawnTime = Time.time;
    //        timeSpawn = Random.Range(spawnTimeRange.x, spawnTimeRange.y);

    //        Vector2 Pos;
    //        Pos.x = xPos;
    //        Pos.y = Random.Range(yRange.x, yRange.y);
    //        platforms[currentIndex].transform.position = Pos;

    //        //float y = Random.Range(yRange.x, yRange.y);
    //        platforms[currentIndex].SetActive(false);
    //        platforms[currentIndex].SetActive(true);

    //        Debug.Log($"[Spawner_Platform] Spawned platform[{currentIndex}] at ({Pos.x}, {Pos.y})");

    //        currentIndex++;

    //        currentIndex = (int)Mathf.Repeat(currentIndex, platformCount);
    //    }
        
    //}

