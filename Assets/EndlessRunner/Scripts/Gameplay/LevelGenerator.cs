using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform levelSpawnPoint;
    [SerializeField] private Transform EnvironmentHolder;

    private EnvironmentMovement environmentMovement;

    [SerializeField] private int totalPatchesToSpawn = 5;

    [Header("Empty patches less than total"), SerializeField]
    private int emptyPatches = 2;
    [SerializeField] private List<Material> patchColors;

    [Header("Platform To Be Spawned"), SerializeField, ContextMenuItem("GeneratePlatformPrefab", "GeneratePlatformPrefab", order = 1)]
    private Platform platformPrefab;

    [SerializeField, Header("True For Infinite Level")]
    private bool isInfiniteLevel = true;
    [Header("Complete Prefab, When Not Infinite"), SerializeField]
    private Transform platformCompletePrefab;
    [Header("When Patches are fixed,Spawn +Shift "), SerializeField]
    private int totalPatchToShift = 5;
    private int totalPatches = 20;
    private bool lastPrefabSpawned = false;


    //where the patch is to be spawned
    private Vector3 nextSpawnPosition;

    private Platform platformPatch;
    private int colorNumber = 0;

    #region Pool Platform
    [SerializeField] private bool isPoolForward = false;

    public List<Platform> PlatformPool = new List<Platform>();
    //last trigger platform
    private Platform lastHitPlatform;
    private int currentHitCounter = 0;
    [SerializeField]
    private int numberOfPlatformShifted = 0;
    private bool isTargetShiftCompleted = false;
    //plaform at the end point
    private Platform endPlatform;
    #endregion


    private void Start()
    {
        emptyPatches = emptyPatches >= totalPatchesToSpawn ? totalPatchesToSpawn / 2 : emptyPatches;
        nextSpawnPosition = levelSpawnPoint.position;

        totalPatches = totalPatchesToSpawn + totalPatchToShift;
        GeneratePlatformPrefab();
        environmentMovement = EnvironmentHolder.transform.parent.GetComponent<EnvironmentMovement>();
    }


    void GeneratePlatformPrefab()
    {
        StartCoroutine(nameof(StartGenerating));
    }

    IEnumerator SpawnPatch(bool isSpawnPatchItems)
    {
        Platform patch = Instantiate(platformPrefab, nextSpawnPosition, Quaternion.identity);
        nextSpawnPosition = patch.Endpoint().position;

        endPlatform = patch;
        //lastHitPlatform = patch;

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

        currentHitCounter++;


        if (!isInfiniteLevel)
        {
            numberOfPlatformShifted++;
            if (numberOfPlatformShifted >= totalPatches - 1 && !isTargetShiftCompleted)
            {
                Debug.Log("Spawn Target Completed");
                isTargetShiftCompleted = true;
                environmentMovement.Movement(true);
            }


        }


        if (currentHitCounter >= 2 && numberOfPlatformShifted <= totalPatchToShift)
        {
            yield return null;

            lastHitPlatform.transform.position = endPlatform.Endpoint().position;
            endPlatform = lastHitPlatform;

            currentHitCounter--;
        }

        lastHitPlatform = patch;
        if (numberOfPlatformShifted > totalPatchToShift && !isTargetShiftCompleted)
            SpawnEndPatch();
    }


    IEnumerator StartGenerating()
    {
        for (int i = 0; i < totalPatchesToSpawn; i++)
        {
            bool generatePatchItem = i >= emptyPatches;
            StartCoroutine(SpawnPatch(generatePatchItem));
            yield return new WaitForEndOfFrame();
        }
    }
    void SpawnEndPatch()
    {
        if (!lastPrefabSpawned)
        {
            lastPrefabSpawned = true;
            Transform patch = Instantiate(platformCompletePrefab, endPlatform.Endpoint().position, Quaternion.identity);
            patch.transform.SetParent(EnvironmentHolder);
        }
    }




}
