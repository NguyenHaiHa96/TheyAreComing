using UnityEngine;

public class AK47BulletSpawner : BulletSpawner
{
    private void OnEnable() 
    {
        InitializeVariables();
    }

    public override void InitializeVariables()
    {    
        weapon = GetComponent<Weapon>();
        bullet = weapon.bulletType;
        CheckWeaponDirection();
    }

    public override void InitializeBullet(Vector3 position)
    {
        CheckWeaponDirection();
        GameObject newBullet = Instantiate(bullet.gameObject, position, Quaternion.identity, GameManager.Instance.BulletStorgage.transform);
        newBullet.GetComponent<Bullet>().OnAddingForce(bulletDirection);
    }

    private void Update()
    {
        
    }
}
