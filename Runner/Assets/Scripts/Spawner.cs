using UnityEngine;
using System.Linq;

public enum PositionType
{
    BOTTOM,
    TOP
}

[System.Serializable]
public class SpawnPosition
{
    [SerializeField]
    private Transform transform;

    [SerializeField]
    PositionType positionType;

    public Transform Transform => transform;
    public PositionType PositionType => positionType;
}

public class Spawner : MonoBehaviour
{
    [SerializeField, Range(0.0f, 10f)]
    private float deltaSpawnTime = 7f;

    [SerializeField, Range(0,0.3f)]
    private float decrementDeltaSpawnTime = 0.1f;

    [SerializeField, Range(0.1f,  1)]
    private float minTimeBetweenSpawns = 0.3f;

    [SerializeField]
    private SpawnPosition[] spawnPositions;

    private float time = 0f;

    private PositionType lastPositionType;
    private int spawnPositionIndex;
    private bool lastInstantiateIsBonus;

    private void Awake()
    {
        lastPositionType = PositionType.BOTTOM;
        lastInstantiateIsBonus = true;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if(time > deltaSpawnTime)
        {
            SpawnObjectFromPool();

            time = 0f;

            if(deltaSpawnTime > minTimeBetweenSpawns)
            {
                deltaSpawnTime -= decrementDeltaSpawnTime;
            }  
        }
    }

    private void SpawnObjectFromPool()
    {
        bool instantiateBonus = ChooseObjectToInstantiate();
        SpawnPosition spawnPosition = ChooseSpawnPosition();

        Debug.Log(spawnPosition.PositionType);

        // if 2 malus bottom when min spawn time 
        if( deltaSpawnTime <= minTimeBetweenSpawns && 
            !lastInstantiateIsBonus == !instantiateBonus &&
            spawnPosition.PositionType == PositionType.BOTTOM &&
            lastPositionType == spawnPosition.PositionType)
        {
            spawnPosition = SpawnTop();
        }
        
        GameObject prefab;
        if (instantiateBonus)
            prefab = PoolingSystem.Instance.GetBonusFromPool();
        else
            prefab = PoolingSystem.Instance.GetMalusFromPool();

        prefab.transform.SetParent(spawnPosition.Transform);
        prefab.transform.position = spawnPosition.Transform.position;

        lastInstantiateIsBonus = instantiateBonus;
        lastPositionType = spawnPosition.PositionType;
    }

    private SpawnPosition ChooseSpawnPosition()
    {
        spawnPositionIndex = Random.Range(0, spawnPositions.Length);
        return spawnPositions[spawnPositionIndex];
    }

    private SpawnPosition SpawnTop()
    {
        return spawnPositions.Single(spawnPosition => spawnPosition.PositionType == PositionType.TOP);
    }

    private bool ChooseObjectToInstantiate()
    {
        return Random.Range(0, 2) == 0;
    }
}
