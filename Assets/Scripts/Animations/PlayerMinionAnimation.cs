using UnityEngine;

public class PlayerMinionAnimation : ObjectAnimation, ISubcribers, IInitializeVariables
{
    [SerializeField] private PlayerAnimationController animationController;
    
    private void Start()
    {
        InitializeVariables();
        SubscribeEvent();
        animator.SetBool("IsRunning", animationController.IsRunning);
    }

    private void OnDisable()
    {
        UnsubscribeEvent();
    }

    public void InitializeVariables()
    {
        animator = GetComponentInChildren<Animator>();
        animationController = PlayerAnimationController.Instance;
    }

    public void SubscribeEvent()
    {
        animationController.OnRunningStateChange += PlayRunningAnimation;
        animationController.OnReachedFinishLine += PlayStandShootingAnimation;
    }

    public void UnsubscribeEvent()
    {
        animationController.OnRunningStateChange -= PlayRunningAnimation;
        animationController.OnReachedFinishLine -= PlayStandShootingAnimation;
    }

    private void PlayRunningAnimation(bool isRunning)
    {
        animator.SetBool("IsRunning", isRunning);
    }

    private void PlayStandShootingAnimation(bool reachedFinishLine)
    {
        animator.SetBool("ReachedFinishLine", reachedFinishLine);
    }
}
