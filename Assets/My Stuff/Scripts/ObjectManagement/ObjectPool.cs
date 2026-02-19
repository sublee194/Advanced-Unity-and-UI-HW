using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.STP;

public class ObjectPool 
{
    private static ObjectPool _instance = null;
    public static ObjectPool Instance() { return _instance; }

    private Dictionary<PoolType, Queue<GameObject>> poolMap;

    public ObjectPool()
    {
        _instance = this;
    }

    // pre-instantiate 所有 ObjectManager 丟過來的池子裡的東西
    public void InitPoolData(PoolType poolType, GameObject prefab, int initialSize)
    {
        //如果被丟過來的池子不在 Dictionary poolMap 裡面，加進去 
        if (!poolMap.ContainsKey(poolType))
        {
            Queue<GameObject> prefabQueue = new Queue<GameObject>();
            poolMap.Add(poolType, prefabQueue);
        }

        //根據池子大小，事先生成一定的數量
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.SetActive(false);
            poolMap[poolType].Enqueue(obj); //把 obj 加進 Dictinoary poolMap 中 key 值為 poolType 的 Queue 裡面
        }
    }

    //別的腳本需要生成物件 (從池子裡撈 prefab) 時，呼叫這個方法
    public GameObject GetPrefabFromPool(PoolType poolType)
    {
        //檢查 Dictionary poolMap 中有沒有該 key 值
        if (!poolMap.ContainsKey(poolType))
        {
            Debug.Log($"Cannot find the key {poolType} in poolMap");
            return null;
        }

        //檢查該池子 (Queue) 還有沒有 prefab 
        if(poolMap.Count == 0)
        {
            Debug.Log($"Pool for {poolType} is empty!");
            return null;
        }

        //若有，將之從 Queue 移除、啟動、並回傳給該腳本
        GameObject prefab = poolMap[poolType].Dequeue();
        IResettable resettable = prefab.GetComponent<IResettable>();
        resettable?.Reset();
        prefab.SetActive(true);
        return prefab;
    }

    public void ReturnPrefabToPool(PoolType poolType, GameObject prefab)
    {
        prefab.SetActive(false);
        poolMap[poolType].Enqueue(prefab);
    }
}
