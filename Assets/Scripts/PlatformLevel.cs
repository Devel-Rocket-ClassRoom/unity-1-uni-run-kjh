using UnityEngine;
 

public class PlatformLevel : MonoBehaviour
{
    public GameObject[] platforms;
    public int level;
    public float spawnInterval = 3f; // Time interval between spawns
    public float spawnTime;
    public Vector2 spawnPosition = new Vector2(20, -1.5f); // Position where platforms will be spawned

    public GameManager gameManager;
    private PlatformPool objectPool;

    private void Start()
    {
        // 1순위: Inspector에서 할당된 것 사용
        // 2순위: 같은 GameObject에서 찾기
        // 3순위: 씬 전체에서 찾기
        if (objectPool == null)
            objectPool = GetComponent<PlatformPool>();
        if (objectPool == null)
            objectPool = FindObjectOfType<PlatformPool>();

        if (objectPool == null)
        {
            Debug.LogError("PlatformPool을 찾을 수 없습니다! 씬에 PlatformPool 컴포넌트가 존재하는지 확인하세요.", this);
            return;
        }

        objectPool.Initialize();
    }
         
    void Update()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }
        spawnTime += Time.deltaTime;
        //if(spawnTime>=2.9f)
        //{
        //    spawnTime = 0f;
        //    level = Random.Range(0, platforms.Length);
        //    if(platforms[level] != null)
        //    {
        //        Instantiate(platforms[level], new Vector2(20, -1.5f), Quaternion.identity);
        //    }
        //}
        if (spawnTime > spawnInterval)
        {
            spawnTime = 0;
            SpawnPlatform();

        }

    }
    private void SpawnPlatform()
    {
        if (objectPool == null) return;

        GameObject lvl = objectPool.GetObject();
        if (lvl == null) return;

        //코인/장애물 초기화
        foreach(Transform child in lvl.transform)
        {
            child.gameObject.SetActive(true);
        }

        lvl.transform.position = spawnPosition;//플랫폼의 위치를 설정
        lvl.SetActive(true);
    }
}
