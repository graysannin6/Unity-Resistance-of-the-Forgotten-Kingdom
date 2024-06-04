using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int poolSize = 100;

    private List<GameObject> pool;

    void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            foreach (GameObject prefab in enemyPrefabs)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(int index)
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy && obj.name.StartsWith(enemyPrefabs[index].name))
            {
                return obj;
            }
        }

        GameObject newObj = Instantiate(enemyPrefabs[index]);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);

        BaseEnemy enemy = obj.GetComponent<BaseEnemy>();
        if (enemy != null)
        {
            enemy.ResetHealth();
        }
    }
}
