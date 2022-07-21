using UnityEngine;

public class WeaponAnimation : ObjectAnimation, ISubcribers, IInitializeVariables
{
    private Weapon weapon;
    private int PickedUpHash;

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
        weapon = GetComponent<Weapon>();
        PickedUpHash = Animator.StringToHash("PickedUp");
    }

    public void SubscribeEvent()
    {
        weapon.OnPickedUp += PickedUpNewWeapon;
    }

    public void UnsubscribeEvent()
    {
        weapon.OnPickedUp -= PickedUpNewWeapon;
    }

    private void PickedUpNewWeapon()
    {
        animator.SetBool(PickedUpHash, true);
    }
}
