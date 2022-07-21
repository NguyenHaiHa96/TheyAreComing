using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMinionAnimation : ObjectAnimation, ISubcribers, IInitializeVariables
{
    public static bool Idling = false;

    private EnemyMinion enemyMinion;

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
        animator = GetComponentInChildren<Animator>();
        enemyMinion = GetComponent<EnemyMinion>();
        animator.SetBool("IsIdling", Idling);
    }

    public void SubscribeEvent()
    {
        enemyMinion.OnIdling += PlayIdlingAnimation;
        enemyMinion.OnDeath += PlayFallingDeathAnimation;
    }

    public void UnsubscribeEvent()
    {
        enemyMinion.OnIdling -= PlayIdlingAnimation;
        enemyMinion.OnDeath -= PlayFallingDeathAnimation;
    }

    private void PlayFallingDeathAnimation()
    {
        animator.SetBool("IsDeath", true);
    }

    private void PlayIdlingAnimation()
    {
        Idling = true;
        animator.SetBool("IsIdling", Idling);
    }
}
