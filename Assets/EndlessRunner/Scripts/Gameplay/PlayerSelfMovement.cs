using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelfMovement : Movement
{
    // [SerializeField, Range(0, 10)] protected float speed = 5;
    [SerializeField] private float limitX;
    [SerializeField] private float limitY;
    private float _finalPosX;
    private float _currentPosX;

    private float _finalPosY;
    private float _currentPosY;
    [SerializeField] float offsetY = 4;

    [SerializeField] private float sidewaySpeed;
    [SerializeField] private float upwardSpeed;
    [SerializeField] private float cameraShiftSpeed;

    [SerializeField] private Transform ObjectToMove;

    private CameraController cameraController;
    private EnvironmentMovement enviroenmentMovement;

    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        enviroenmentMovement = GameObject.FindObjectOfType<EnvironmentMovement>();

    }
    public override void Update()
    {
        if (isStart)
        {
            StartMovement();
            CameraBoostEffect();
            StartParticles();
        }

        else
        {
            startTime -= Time.deltaTime;
            if (startTime <= 0)
                isStart = true;
        }
    }


    public override void StartMovement()
    {
        if (Input.GetMouseButton(0))
        {
            HorizontalMovement();
            VerticalMovement();
        }
        else
            _finalPosY = 0;

        ObjectToMove.localPosition = new Vector3(SmoothXMovement(), ObjectToMove.position.y, offsetY + SmoothYMovement());
    }

    #region Horizontal Movement

    private void HorizontalMovement()
    {
        float percentageX = (Input.mousePosition.x - Screen.width / 2) / (Screen.width * 0.5f) * 2;
        percentageX = Mathf.Clamp(percentageX, -1.0f, 1.0f);
        _finalPosX = percentageX * limitX;
    }

    private float SmoothXMovement()
    {
        float delta = _finalPosX - _currentPosX;
        _currentPosX += (delta * Time.deltaTime * sidewaySpeed);
        _currentPosX = Mathf.Clamp(_currentPosX, -limitX, limitX);

        return _currentPosX;
    }

    #endregion


    #region Vertical Movement

    private void VerticalMovement()
    {
        float percentageY = (Input.mousePosition.y - Screen.height / 2) / (Screen.height * 0.2f);
        percentageY = Mathf.Clamp(percentageY, 0, 1);
        _finalPosY = percentageY * limitY;
    }

    private float SmoothYMovement()
    {
        float deltaY = _finalPosY - _currentPosY;
        _currentPosY += (deltaY * Time.deltaTime * upwardSpeed*0.7f);
        _currentPosY = Mathf.Clamp(_currentPosY, 0, limitY);

        return _currentPosY;
    }

    #endregion

    #region

    private void CameraBoostEffect()
    {
        bool boostEffect = _currentPosY > 0.01f;

        if (boostEffect)
        {
            cameraController.CameraBoostEffect(_currentPosY * upwardSpeed*0.8f);
        }
        enviroenmentMovement.SetSpeed(_currentPosY*upwardSpeed);
        cameraController.BoostParticle(boostEffect);
    }
    #endregion


    void StartParticles()
    {
        cameraController.StartParticle();
    }

}
