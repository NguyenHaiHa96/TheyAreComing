using System;
using UnityEngine;

public class PlayerAnimationController : Manager<PlayerAnimationController>, ISubcribers
{
    public static PlayerAnimationController animationController;

    public event Action<bool> OnRunningStateChange = delegate { };
    public event Action<bool> OnReachedFinishLine = delegate { };

    private PlayerMain playerMain;
    private bool isRunning;
    private bool reachedFinishLine;

    public bool IsRunning { get => isRunning; set => isRunning = value; }
    public bool ReachedFinishLine { get => reachedFinishLine; set => reachedFinishLine = value; }

    private void Awake()
    {
        InitializeSingleton();
        InitializeVariables();
    }

    private void OnEnable()
    {
        SubscribeEvent();
    }

    private void OnDisable()
    {
        UnsubscribeEvent();
    }

    public void SubscribeEvent()
    {
        playerMain.OnRunning += RunningState;
        playerMain.OnReachedFinishLine += StandShootingState;
    }
    public void UnsubscribeEvent()
    {
        playerMain.OnRunning -= RunningState;
        playerMain.OnReachedFinishLine -= StandShootingState;
    }

    private void StandShootingState(GameObject[] gameObjects)
    {
        reachedFinishLine = true;
        OnReachedFinishLine?.Invoke(reachedFinishLine);
    }

    private void RunningState()
    {
        isRunning = true;
        OnRunningStateChange?.Invoke(isRunning);
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
        playerMain = GetComponent<PlayerMain>();
    }
}
