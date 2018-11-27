using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{

    public int WorldSize;
    [SerializeField] private int terrainOffset = 500;
    [SerializeField] private GameObject terrain;
    [SerializeField] private GameObject terrainParent;
    [SerializeField] private GameObject onWorldObjectsParent;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject cactusPrefab;
    [SerializeField] private GameObject rockPrefab1;
    [SerializeField] private GameObject rockPrefab2;
    [SerializeField] private GameObject skullPrefab;

    [Range(0, 200)] [SerializeField] private int cactusAmount;
    [Range(0, 200)] [SerializeField] private int rock1Amount;
    [Range(0, 200)] [SerializeField] private int rock2Amount;
    [Range(0, 200)] [SerializeField] private int skullAmount;

    public void Start()
    {
        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        for (int i = 0; i <= WorldSize; i++)
        {
            for (int j = 0; j <= WorldSize; j++)
            {
                var InstantiatedTerrain = Instantiate(terrain, new Vector3(j * terrainOffset, 0, i * terrainOffset), Quaternion.identity);
                InstantiatedTerrain.transform.parent = terrainParent.transform;
            }
        }

        var PlayerObject = Instantiate(player, new Vector3((WorldSize*terrainOffset)/2,0, (WorldSize * terrainOffset) / 2), Quaternion.identity);
        PlayerObject.name = "Player";

        SpawnOnWorldObjects(cactusPrefab, cactusAmount);
        SpawnOnWorldObjects(rockPrefab1, rock1Amount);
        SpawnOnWorldObjects(rockPrefab2, rock2Amount);
        SpawnOnWorldObjects(skullPrefab, skullAmount);
    }

    public void SpawnOnWorldObjects(GameObject objectToSpawn, int amount)
    {
        for (int k = 0; k <= (amount*WorldSize); k++)
        {
            var InstantiatedObject = Instantiate(objectToSpawn, new Vector3(Random.Range(0, WorldSize * terrainOffset), -0.1f, Random.Range(0, WorldSize * terrainOffset)), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            InstantiatedObject.transform.parent = onWorldObjectsParent.transform;
        }
    }
}
