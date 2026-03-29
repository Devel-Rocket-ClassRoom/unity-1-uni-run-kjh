using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlatformPool : MonoBehaviour
{

    public GameObject[] prefab; // The prefab to be pooled
    public int countPerPrefab = 3;

    private List<GameObject> pool = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Initialize()
    {
        foreach (GameObject prefabs in prefab)
        {
            for (int i = 0; i < countPerPrefab; i++)
            {
                GameObject lvl = Instantiate(prefabs);// 프리팹을 인스턴스화하여 새로운 게임 오브젝트를 생성
                lvl.SetActive(false); // Deactivate the object when it's created
                pool.Add(lvl); // Add the object to the pool
            }
        }
    }
    public GameObject GetObject()
    {
        List<GameObject> available = new List<GameObject>();

        foreach (GameObject lvl in pool)
        {
            if (!lvl.activeInHierarchy)
            {
                available.Add(lvl);
            }
        }

        if (available.Count > 0)
        {
            int index = Random.Range(0, available.Count);
            return available[index];

        }
        else
        {
            Debug.LogWarning("pool X");
            return null;
        }
       
    }
    public void ReturnObject(GameObject lvl)
    {
        lvl.SetActive(false); // Deactivate the object when it's returned to the pool
    }
}
