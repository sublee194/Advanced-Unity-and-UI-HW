using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour
{
    private float spawnTimer;
    public float spawnTime; //每隔多久會生出蜘蛛
    public int initialSpawnNum; //每一個地點一開始生成出多少隻蜘蛛

    [SerializeField] private LayerMask spawnBlockingMask; //哪些 layer 會阻擋蜘蛛生成
    [SerializeField] private float spawnCheckRadius = 1.5f; //檢查的 Sphere 半徑
    [SerializeField] private int maxSpawnAttempts = 10; //檢查的次數上限

    public List<Transform> spawnLocations = new List<Transform>(); //所有的生成點清單

    void Awake()
    {
        spawnTimer = spawnTime;
    }

    void Start()
    {
        StartCoroutine(SpawnInitialWave());
    }

    private IEnumerator SpawnInitialWave()
    {      
        foreach (Transform location in spawnLocations)
        {
            for (int i = 0; i < initialSpawnNum; i++)
            {                
                bool isValidSpawnPoint = TryGetValidSpawnPosition(location.position, out Vector3 pos);
                if(isValidSpawnPoint)
                {
                    PoolType randSpider = GetRandomSpiderType();
                    GameObject spider = ObjectPool.Instance().GetPrefabFromPool(randSpider);

                    spider.transform.position = pos;
                    float randomYAngle = Random.Range(0f, 360f);
                    spider.transform.rotation = Quaternion.Euler(0f, randomYAngle, 0f);
                    spider.SetActive(true);
                }
                //Vector3 randPosOffset = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));
                //spider.transform.position = location.position + randPosOffset;
                                
                yield return null;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnRandomSpider();
    }

    void SpawnRandomSpider()
    {
        
        if (spawnTimer <= 0)
        {
            PoolType randSpider = GetRandomSpiderType();
            Transform randSpawnPoint = GetRandomSpawnLocation();
            bool isValidSpawnPoint = TryGetValidSpawnPosition(randSpawnPoint.position, out Vector3 pos);
            if(isValidSpawnPoint)
            {
                GameObject spider = ObjectPool.Instance().GetPrefabFromPool(randSpider);
                if (spider == null)
                {
                    return;
                }

                spider.transform.position = pos;
                float randomYAngle = Random.Range(0f, 360f);
                spider.transform.rotation = Quaternion.Euler(0f, randomYAngle, 0f);

                spider.SetActive(true);
            }
            
            spawnTimer = spawnTime;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }

    PoolType GetRandomSpiderType()
    {
        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                return PoolType.SpiderMonster_1;
            case 1:
                return PoolType.SpiderMonster_2;
            default:
                return PoolType.SpiderMonster_3;
        }
    }

    Transform GetRandomSpawnLocation()
    {
        int randPoint = Random.Range(0, spawnLocations.Count);
        Transform randSpawnPoint = spawnLocations[randPoint];
        if(randSpawnPoint == null || spawnLocations.Count == 0)
        {
            Debug.LogError("No spawn points assigned.");
            return null;
        }
        Vector3 randPosOffset = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));
        randSpawnPoint.position += randPosOffset;
        return randSpawnPoint;
    }

    bool TryGetValidSpawnPosition(Vector3 basePoint, out Vector3 validPos)
    {
        for (int i = 0;  i < maxSpawnAttempts; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));
            Vector3 candidate = basePoint + randomOffset;

            bool blocked = Physics.CheckSphere(candidate, spawnCheckRadius, spawnBlockingMask);
            if(!blocked)
            {
                validPos = candidate;
                return true;
            }
        }

        Debug.LogError("Can't find spawn location!");
        validPos = Vector3.zero;
        return false;
    }
}
