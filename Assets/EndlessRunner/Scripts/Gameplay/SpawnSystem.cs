using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnSystem : MonoBehaviour
{
    public GameObject prefab;
    [SerializeField] private GameObject[] spawnPrefabs;
    public BoxCollider spawnArea;
    public int numObjects = 10;
    public float minDistance = 1f;
    private List<Vector3> spawnPositions;
    [SerializeField] private List<GameObject> spawnedObjects;

    void Start()
    {
        // Get the bounds of the spawn area
        Vector3 bounds = spawnArea.bounds.size;

        // Calculate the number of objects that can fit inside the spawn area
        int numObjectsFit = Mathf.FloorToInt((bounds.x * bounds.y * bounds.z) / (minDistance * minDistance * minDistance));

        // Make sure we don't try to spawn more objects than can fit inside the spawn area
        numObjects = Mathf.Min(numObjects, numObjectsFit);


        // Generate a list of random positions to spawn objects at
        //spawnPositions = GenerateSpawnPositions(numObjects, minDistance, new Vector3(transform.position.x, transform.position.y, spawnArea.bounds.min.z), new Vector3(transform.position.x, transform.position.y, spawnArea.bounds.max.z));
        spawnPositions = GenerateSpawnPositions(numObjects, minDistance, new Vector3(spawnArea.bounds.min.x, transform.position.y, spawnArea.bounds.min.z), new Vector3(spawnArea.bounds.max.x, transform.position.y, spawnArea.bounds.max.z));


        // Spawn the objects at the generated positions
        for (int i = 0; i < numObjects; i++)
        {

            spawnedObjects.Add(Instantiate(spawnPrefabs[Random.Range(0, spawnPrefabs.Length)], spawnPositions[i], Quaternion.identity));
            spawnedObjects[i].transform.SetParent(this.transform);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            ResetBalloons();
    }
    public void ResetBalloons()
    {
        for (int i = 0; i < numObjects; i++)
        {
            if (spawnedObjects.Count >= numObjects)
            {
                spawnedObjects[i].transform.position = spawnPositions[i];
            }

        }
    }

    List<Vector3> GenerateSpawnPositions(int numPositions, float minDistance, Vector3 minBounds, Vector3 maxBounds)
    {
        List<Vector3> positions = new List<Vector3>();
        int numTries = 0;

        // Try to generate numPositions number of spawn positions that are at least minDistance away from each other
        while (positions.Count < numPositions && numTries < 1000)
        {
            Vector3 position = new Vector3(Random.Range(minBounds.x, maxBounds.x),
                                           Random.Range(minBounds.y, maxBounds.y),
                                           Random.Range(minBounds.z, maxBounds.z));

            // Check if the new position is at least minDistance away from all existing positions
            bool validPosition = true;
            foreach (Vector3 existingPosition in positions)
            {
                if (Vector3.Distance(existingPosition, position) < minDistance)
                {
                    validPosition = false;
                    break;
                }
            }

            // If the new position is valid, add it to the list of spawn positions
            if (validPosition)
            {
                positions.Add(position);
            }

            numTries++;
        }

        return positions;
    }
}
