using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject bonusPrefab;

    [SerializeField]
    private GameObject malusPrefab;

    private Queue<GameObject> availableBonusObjects = new Queue<GameObject>();
    private Queue<GameObject> availableMalusObjects = new Queue<GameObject>();

    public static PoolingSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GrowPool(bonusPrefab, availableBonusObjects);
        GrowPool(malusPrefab, availableMalusObjects);
    }

    public GameObject GetBonusFromPool()
    {
        if (availableBonusObjects.Count == 0)
            GrowPool(bonusPrefab, availableBonusObjects);

        var instance = availableBonusObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
    public GameObject GetMalusFromPool()
    {
        if (availableBonusObjects.Count == 0)
            GrowPool(malusPrefab, availableMalusObjects);

        var instance = availableMalusObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }

    private void GrowPool(GameObject prefab, Queue<GameObject> queue)
    {
        for (int i = 0; i < 10; i++)
        {
            var instanceToAdd = Instantiate(prefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd, queue);
        }
    }

    private void AddToPool(GameObject prefab, Queue<GameObject> queue)
    {
        prefab.SetActive(false);
        queue.Enqueue(prefab);
    }

    public void AddToBonusPool(GameObject prefab) => AddToPool(prefab, availableBonusObjects);
    public void AddToMalusPool(GameObject prefab) => AddToPool(prefab, availableMalusObjects);
}
