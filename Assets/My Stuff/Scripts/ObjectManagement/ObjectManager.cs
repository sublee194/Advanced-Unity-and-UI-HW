using System.Collections.Generic;
using UnityEngine;
using static ObjectPool;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager _instance = null;
    public static ObjectManager Instance() { return _instance; }

    [Header("Prefabs for Pool")]
    public GameObject SciFiVan_1_Prefab; 
    public GameObject SciFiVan_2_Prefab;
    public GameObject monsterPrefab;
    public GameObject ammoPrefab;
    public GameObject magazinePrefab;

    [System.Serializable]
    public class PoolConfig
    {
        public PoolType poolType; //哪種物件的資源池
        public GameObject prefab; //物件的 prefab
        public int initialSize; //資源池大小
    }

    private List<PoolConfig> poolConfigs;

    private void Awake()
    {
        _instance = this;

        // 先 new 出所有需要用的的資源池種類
        PoolConfig SciFiVanPool_1 = new PoolConfig { poolType = PoolType.SciFiVan_1, prefab = SciFiVan_1_Prefab, initialSize = 10 };
        PoolConfig SciFiVanPool_2 = new PoolConfig { poolType = PoolType.SciFiVan_2, prefab = SciFiVan_2_Prefab, initialSize = 10 };
        PoolConfig monsterPool = new PoolConfig { poolType = PoolType.Monster, prefab = monsterPrefab, initialSize = 20 };
        PoolConfig ammoPool = new PoolConfig { poolType = PoolType.Ammo, prefab = ammoPrefab, initialSize = 50 };
        PoolConfig magazinePool = new PoolConfig { poolType = PoolType.SciFiVan_1, prefab = magazinePrefab, initialSize = 20 };

        //把他們加進 List 
        poolConfigs.Add(SciFiVanPool_1);
        poolConfigs.Add(SciFiVanPool_2);
        poolConfigs.Add(monsterPool);
        poolConfigs.Add(ammoPool);
        poolConfigs.Add(magazinePool);

        //呼叫 ObjectPool ==> 由它負責 pre-instantiate 池子裡的東西
        foreach (var config in poolConfigs)
        {
            ObjectPool.Instance().InitPoolData(config.poolType, config.prefab, config.initialSize);
        }
    }
}
