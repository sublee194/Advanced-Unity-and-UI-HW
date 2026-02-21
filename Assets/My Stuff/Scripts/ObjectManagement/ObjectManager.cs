using System.Collections.Generic;
using UnityEngine;
using static ObjectPool;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager _instance = null;
    public static ObjectManager Instance() { return _instance; }

    [System.Serializable]
    public class PoolConfig
    {
        public PoolType poolType; //哪種物件的資源池
        public GameObject prefab; //物件的 prefab
        public int initialSize; //資源池大小
    }

    [Header("Pool Settings")]
    [SerializeField] private List<PoolConfig> poolConfigs = new List<PoolConfig>(); //透過 Inspector 去編輯不同池子的參數

    private void Awake()
    {
        _instance = this;
        
        //呼叫 ObjectPool ==> 由它負責 pre-instantiate 池子裡的東西
        foreach (var config in poolConfigs)
        {
            ObjectPool.Instance().InitPoolData(config.poolType, config.prefab, config.initialSize);
        }
    }
}
