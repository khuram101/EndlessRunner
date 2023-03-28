using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : PoolPatch, IDamage
{

    public override void Initialize(LevelGenerator _levelGenerator)
    {
        levelGenerator = _levelGenerator;

    }

    public void ShiftToNextPoint()
    {
        _ = StartCoroutine(levelGenerator.ShiftPlatform(GetComponent<Platform>()));
    }



    public override Transform Endpoint()
    {
        return base.Endpoint();
    }

    public override void SetMaterial(Material material)
    {
        base.SetMaterial(material);
    }


    #region  Pool Mechanics

    public void OnDamage()
    {
        //throw new System.NotImplementedException();
    }

    public void OnDamage(float amount)
    {
        // throw new System.NotImplementedException();
    }


    #endregion
}
