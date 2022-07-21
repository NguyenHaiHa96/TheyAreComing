using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public WeaponInfomation weaponInfo;
    public Rigidbody rb;
    public float timeDelay;
    public int gunDamage;

    public void DestroyBullet() 
    {
        Destroy(gameObject, timeDelay);
    }

    public abstract void InitializeVariables();

    public void InstantiateModel()
    {
        Instantiate(weaponInfo.bulletModel, transform.position, Quaternion.identity, transform);
    }
    public void OnAddingForce(Vector3 bulletDirection)
    {
        rb.AddForce(weaponInfo.BulletMoveSpeed * bulletDirection.normalized, ForceMode.VelocityChange);      
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("EnemyMinion"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null) 
            {
                damageable.TakeDamage(gunDamage);
                Destroy(gameObject);
            }
        }
    }
}
