using UnityEngine;

public class AK47Bullet : Bullet
{
    public override void InitializeVariables()
    {
        gunDamage = weaponInfo.damage;
        rb = GetComponent<Rigidbody>();
        timeDelay = 3f;
    }

    private void OnEnable()
    {
        InitializeVariables();     
        InstantiateModel();
    }

    private void Update() 
    {
        DestroyBullet();
    }
}
