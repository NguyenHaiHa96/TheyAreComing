using UnityEngine;

public abstract class BulletSpawner : MonoBehaviour
{
    public Weapon weapon;
    public Bullet bullet;
    public Vector3 bulletDirection;

    public void CheckWeaponDirection()
    {
        bulletDirection = this.gameObject.transform.forward; 
    }

    public abstract void InitializeVariables();

    public abstract void InitializeBullet(Vector3 position);
}
