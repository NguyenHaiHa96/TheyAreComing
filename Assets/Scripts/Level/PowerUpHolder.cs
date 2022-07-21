using System;
using UnityEngine;

public class PowerUpHolder : MonoBehaviour, ISubcribers
{
    public event Action OnDeactivateHolder = delegate { };

    [SerializeField] private PowerUp powerUp1;
    [SerializeField] private PowerUp powerUp2;

    private void OnEnable()
    {
        SubscribeEvent();
    }

    private void OnDisable()
    {
        UnsubscribeEvent();
    }

    private void DeactivateHolder()
    {
        OnDeactivateHolder?.Invoke();
    }

    public void SubscribeEvent()
    {
        powerUp1.OnDeactivate += DeactivateHolder;
        powerUp2.OnDeactivate += DeactivateHolder;
    }

    public void UnsubscribeEvent()
    {
        powerUp1.OnDeactivate -= DeactivateHolder;
        powerUp2.OnDeactivate -= DeactivateHolder;
    }
}
