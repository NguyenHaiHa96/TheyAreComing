using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHolderAnimation : ObjectAnimation, ISubcribers, IInitializeVariables
{
    private PowerUpHolder powerUpHolder;
    private int deactivateHash;

    private void OnEnable()
    {
        InitializeVariables();
        SubscribeEvent();
    }

    private void OnDisable()
    {
        UnsubscribeEvent();
    }

    public void InitializeVariables()
    {
        animator = GetComponent<Animator>();
        powerUpHolder = GetComponent<PowerUpHolder>();
        deactivateHash = Animator.StringToHash("Deactivate");
    }

    public void SubscribeEvent()
    {
        powerUpHolder.OnDeactivateHolder += DeactivateHolderAnim;
    }

    public void UnsubscribeEvent()
    {
        powerUpHolder.OnDeactivateHolder -= DeactivateHolderAnim;
    }
     
    private void DeactivateHolderAnim()
    {
        animator.SetBool(deactivateHash, true);
    }   
}
