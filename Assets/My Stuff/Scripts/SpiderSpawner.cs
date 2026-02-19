using UnityEngine;

public class SpiderSpawner : MonoBehaviour
{
    private float spawnTimer;
    public float spawnTime; //每隔多久會生出蜘蛛
    
    void Awake()
    {
        spawnTimer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnRandomSpider();
    }

    void SpawnRandomSpider()
    {
        PoolType randSpider = GetRandomSpiderType();

        if (spawnTimer <= 0)
        {
            GameObject spider = ObjectPool.Instance().GetPrefabFromPool(randSpider);
            if (spider == null)
            {
                return;
            }

            Vector3 vRandomPos = new Vector3(Random.Range(-10.0f, 10f), 0f, Random.Range(-10.0f, 10.0f));
            spider.transform.position = transform.position + vRandomPos;
            float randomYAngle = Random.Range(0f, 360f);
            spider.transform.rotation = Quaternion.Euler(0f, randomYAngle, 0f);
            spider.SetActive(true);
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
}
