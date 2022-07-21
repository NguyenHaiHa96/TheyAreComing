using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour, ISubcribers
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dersiredDuration;

    private CharacterController controller;
   private Vector3 turnDirection;
    private Vector3 direction;
    private Quaternion cameraRotation;
    private Quaternion desiredRotation;
    private float elapsedTime;
    private float percentageComplete;
    private bool gameStarted;
    private bool turning;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private void Start()
    {
        SubscribeEvent();
    }

    private void OnEnable()
    {
        InitializeVariables();
    }

    private void OnDisable()
    {
        UnsubscribeEvent();
    }

    private void InitializeVariables()
    {
        controller = GetComponent<CharacterController>();
        direction = Vector3.back;
        gameStarted = false;
    }

    private void Update()
    {
        if (!gameStarted)
        {
            return;
        }
        if (turning)
        {
            SmoothTurning();
        }
        Move();
    }

    private void Move()
    {
         controller.Move(moveSpeed * Time.deltaTime * -(transform.forward).normalized);
    }

    public void SubscribeEvent()
    {
        GameManager.Instance.OnGameStarted += GameStarted;
    }

    public void UnsubscribeEvent()
    {
        GameManager.Instance.OnGameStarted -= GameStarted;
    }

    public void GameStarted(int obj)
    {
        gameStarted = true;
    }

    private void OnTriggerEnter(Collider other) 
    {
        #region Cross
        if (other.CompareTag("Cross"))
        {
            if (other.TryGetComponent(out Cross cross))
            {            
                turnDirection = cross.GetTurnDirection();
                cameraRotation = transform.rotation;
                desiredRotation = Quaternion.Euler(turnDirection);
                turning = true;             
            }
        }
        #endregion
    }

    private void SmoothTurning()
    {
        elapsedTime += Time.deltaTime;
        percentageComplete = elapsedTime / dersiredDuration;
        transform.rotation = Quaternion.Lerp(cameraRotation, desiredRotation, percentageComplete);
        if ( transform.rotation == desiredRotation)
        {            
            turning = false;
            elapsedTime = 0f;
            percentageComplete = 0f;
        }
    }
}
