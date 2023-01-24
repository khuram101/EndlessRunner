using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform levelSpawnPoint;
    [SerializeField] private Transform EnvironmentHolder;
    [SerializeField] private int totalNumberOfPatches = 5;
    [Header("Empty patches less than total"), SerializeField]
    private int emptyPatches = 2;
    [SerializeField] private List<Material> patchColors;

    [Header("Platform To Be Spawned"), SerializeField, ContextMenuItem("GeneratePlatformPrefab", "GeneratePlatformPrefab", order = 1)]
    private Platform platformPrefab;

    //where the patch is to be spawned
    private Vector3 nextSpawnPosition;

    private Platform platformPatch;
    private int colorNumber = 0;

    #region Pool Platform
    public List<Platform> PlatformPool = new List<Platform>();
    //last trigger platform
    private Platform lastHitPlatform;
    private int hitCounter = 0;

    #endregion


    private void Start()
    {
        emptyPatches = emptyPatches >= totalNumberOfPatches ? totalNumberOfPatches / 2 : emptyPatches;
        nextSpawnPosition = levelSpawnPoint.position;

        GeneratePlatformPrefab();
    }


    void GeneratePlatformPrefab()
    {
        StartCoroutine(nameof(StartGenerating));
    }

    IEnumerator SpawnPatch(bool isSpawnPatchItems)
    {
        Platform patch = Instantiate(platformPrefab, nextSpawnPosition, Quaternion.identity);
        nextSpawnPosition = patch.Endpoint().position;

        patch.transform.SetParent(EnvironmentHolder);
        PlatformPool.Add(patch);
        patch.SetMaterial(patchColors[colorNumber]);
        patch.Initialize(GetComponent<LevelGenerator>());


        if (colorNumber >= patchColors.Count - 1)
            colorNumber = 0;
        else
            colorNumber++;


        //delay for patch items generation
        yield return new WaitForEndOfFrame();
        if (isSpawnPatchItems)
        {

        }
    }
    public IEnumerator ShiftPlatform(Platform patch)
    {
        hitCounter++;

        if (hitCounter >= 2)
        {
            yield return new WaitForEndOfFrame();
            lastHitPlatform.transform.position = nextSpawnPosition;
            nextSpawnPosition = lastHitPlatform.Endpoint().position;
            hitCounter--;
        }

        lastHitPlatform = patch;

    }


    IEnumerator StartGenerating()
    {
        for (int i = 0; i < totalNumberOfPatches; i++)
        {
            bool generatePatchItem = i >= emptyPatches;
            StartCoroutine(SpawnPatch(generatePatchItem));
            yield return new WaitForEndOfFrame();
        }

    }





}
