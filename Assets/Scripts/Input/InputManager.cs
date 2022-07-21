using System;
using UnityEngine;

public class InputManager : Manager<InputManager>, ISubcribers
{
    private GameManager gameManager;
    private PlayerInput inputActions;
    private Vector3 movementInput;
    private bool gameStarted;

    public Vector3 MovementInput { get => movementInput; set => movementInput = value; }

    private void Awake() 
    {
        InitializeSingleton();
        inputActions = new PlayerInput();

    }

    private void Start() 
    {
        InitializeVariables();
        SubscribeEvent();
    }

    private void OnDisable() 
    {
        UnsubscribeEvent();
        inputActions.Disable();
    }

     public override void InitializeSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public override void InitializeVariables()
    {
        gameManager = GameManager.Instance;
        gameStarted = false;
        inputActions.Enable();
    }

    public void SubscribeEvent()
    {
        inputActions.Player.Touch.started += cxt => FingerPressed(cxt);
    }

    public void UnsubscribeEvent()
    {
        inputActions.Player.Touch.started -= cxt => FingerPressed(cxt);
    }

    private void FingerPressed(UnityEngine.InputSystem.InputAction.CallbackContext cxt)
    {
        if (!gameStarted)
        {
            if (!gameManager.GameStarted)
            {
                gameManager.GameStarted = true;
            }
        }
        gameStarted = true;
    }

    private void Update() 
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        movementInput = inputActions.Player.Move.ReadValue<Vector2>();
    }
}
