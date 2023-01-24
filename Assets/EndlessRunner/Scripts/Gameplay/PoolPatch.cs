using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolPatch : MonoBehaviour
{
    [SerializeField]
    protected Transform endPoint;

    [SerializeField]
    protected MeshRenderer meshComponent;
    protected LevelGenerator levelGenerator;


    public virtual void Initialize(LevelGenerator _levelGenerator)
    {
        levelGenerator = _levelGenerator;
    }

    //end point of patch to spawn next patch
    public virtual Transform Endpoint()
    {
        return endPoint;
    }

    public virtual void SetMaterial(Material material)
    {
        meshComponent.material = material;
    }

}

