using UnityEngine;

public class AUGBullet : Bullet
{
    public override void InitializeVariables()
    {
        rb = GetComponent<Rigidbody>();
        gunDamage = weaponInfo.damage;
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
