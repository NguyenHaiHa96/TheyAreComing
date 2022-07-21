using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUGBulletSpawner : BulletSpawner
{
    private Vector3 firstBulletDirection;
    private Vector3 thirdBulletDirection;
    private Vector3 secondBulletDirection;
    private Quaternion firstBulletRotation;
    private Quaternion thirdBulletRotation;

    private void OnEnable() 
    {
       InitializeVariables();
    }

    public override void InitializeVariables()
    {
        CheckWeaponDirection();
        BulletDirectionAndRotation();
        weapon = GetComponent<Weapon>();
        bullet = weapon.bulletType;
    }

    public override void InitializeBullet(Vector3 position)
    {
        CheckWeaponDirection();
        BulletDirectionAndRotation();
        GameObject firstBullet = Instantiate(bullet.gameObject, position, firstBulletRotation, GameManager.Instance.BulletStorgage.transform);
        firstBullet.GetComponent<Bullet>().OnAddingForce(firstBulletDirection);
        firstBullet.name = "FirstBullet";
        GameObject secondBullet = Instantiate(bullet.gameObject, position, Quaternion.identity, GameManager.Instance.BulletStorgage.transform);
        secondBullet.GetComponent<Bullet>().OnAddingForce(bulletDirection);
        secondBullet.name = "SecondBullet";
        GameObject thirdBullet = Instantiate(bullet.gameObject, position, thirdBulletRotation, GameManager.Instance.BulletStorgage.transform);
        thirdBullet.GetComponent<Bullet>().OnAddingForce(thirdBulletDirection);
        thirdBullet.name = "ThirdBullet";
    }

    private void BulletDirectionAndRotation()
    {
        firstBulletDirection = new Vector3(bulletDirection.x + 0.2f, bulletDirection.y, bulletDirection.z);
        thirdBulletDirection = new Vector3(bulletDirection.x - 0.2f, bulletDirection.y, bulletDirection.z);
        firstBulletRotation = Quaternion.Euler(firstBulletDirection);
        thirdBulletRotation = Quaternion.Euler(thirdBulletDirection);
    }


    private void Update()
    {
        
    }
}
