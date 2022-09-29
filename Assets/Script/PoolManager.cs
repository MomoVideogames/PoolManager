using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Pool
{
    public string id;
    public int size;
    public GameObject prefab;
}

public class PoolManager : Singleton<PoolManager>
{

    [SerializeField]
    private Pool[] pools;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject pollito = SpawnElement("Pollito");
            pollito.transform.position = new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-1f, 1f));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameObject hierva = SpawnElement("Hierva");
            hierva.transform.position = new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-1f, 1f));

        }
    }

    private void Init()
    {

        if (pools == null)
            return;

        for (int i = 0; i < pools.Length; i++)
        {
            //Create the pool folder
            GameObject go = new GameObject(pools[i].id);
            go.transform.parent = gameObject.transform;

            //Instantiate as many elements in each pool
            for (int j = 0; j < pools[i].size; j++)
            {
                AddElement(pools[i].id, pools[i].prefab);
            }
        }

    }

    /// <summary>
    /// Add a element to a pool
    /// </summary>
    /// <param name="poolId">The name of the pool</param>
    /// <param name="prefab">The prefab which contains the pool</param>
    private void AddElement(string poolId, GameObject prefab )
    {
        Transform parent = transform.Find(poolId);
        GameObject go = Instantiate(prefab, parent) as GameObject;
        go.SetActive(false);
    }

    /// <summary>
    /// Add a element to a pool when the pool is full. This method looks for the prefab automatically. This method is ideal to add element when the pool is full during runtime.
    /// </summary>
    /// <param name="poolId">The name of the pool</param>
    /// <param name="isActive">The initial state of the element when is added to the pool</param>
    /// <returns>The added element. Maybe you want to set some properties of the added element</returns>
    private GameObject AddElement(string poolId, bool isActive = true ) {

        Transform parent = transform.Find(poolId);
        GameObject prefab = pools.FirstOrDefault<Pool>(x => x.id == poolId).prefab;
        GameObject go = Instantiate(prefab, parent) as GameObject;
        go.SetActive(isActive);

        return go;
    }

    public GameObject SpawnElement(string poolId)
    {

        Transform poolTransform = transform.Find(poolId);
        int numElements = transform.Find(poolId).childCount;

        for (int i = 0; i < numElements; i++)
        {
            if (poolTransform.GetChild(i).gameObject.activeSelf == false)
            {
                poolTransform.GetChild(i).gameObject.SetActive(true);
                return poolTransform.GetChild(i).gameObject;
            }
        }

        //If we reach this code means there are no element available, lets add a new one and increase the pool size
        Debug.Log("Pool limit reached, we will increase the pool size my lord");
        return AddElement(poolId,true);

    }

    public GameObject GetPool(string poolId) {
        return transform.Find(poolId).gameObject;
    }
}
