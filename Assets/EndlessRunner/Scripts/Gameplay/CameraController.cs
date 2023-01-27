using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private float currentFieldOfView;
    [SerializeField] private float cameraShiftSpeed;

    [SerializeField] private ParticleSystem boostParticle;
    public ParticleSystemRenderer renderMode;
    private int defaultParticles;
    private float defaultLengthParticles;

    private bool IsEmitionStarted = false;

    void Start()
    {
        currentFieldOfView = mainCamera.fieldOfView;
        renderMode = boostParticle.transform.GetComponent<ParticleSystemRenderer>();
    }


    #region

    public void CameraBoostEffect(float _forwardValue)
    {
        mainCamera.fieldOfView = currentFieldOfView + (_forwardValue * cameraShiftSpeed);
    }
    #endregion

    //Idle // run // sprint

    public void StartParticle()
    {
        if (!IsEmitionStarted)
        {
            IsEmitionStarted = true;
            defaultParticles = 50;
            defaultLengthParticles = 30;
            boostParticle.Play();
        }

    }
    public void BoostParticle(bool isBoost)
    {
        var main = boostParticle.main;

        if (isBoost)
        {
            main.maxParticles = defaultParticles * 2;
            renderMode.lengthScale = defaultLengthParticles * 2;
        }
        else
        {
            main.maxParticles = defaultParticles;
            renderMode.lengthScale = defaultLengthParticles;
        }
    }
}
