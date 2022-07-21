using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, ISubcribers
{
    public event Action OnPickedUp = delegate { };

    public BoxCollider boxCollider;
    public Bullet bulletType;
    public GameManager gameManager;
    public PlayerMain playerMain;
    public BulletSpawner bulletSpawner;
    public WeaponInfomation weaponInfo;
    public float nextFire;
    public bool initializedModel;
    public bool gameStarted;
    public bool firing;
    public bool pickedUp;

    public void InitializeModel()
    {
        if (initializedModel)
        {
            return;
        }
        Instantiate(weaponInfo.gunModel, transform);
    }

    public void InitializeVariables()
    {
        boxCollider = GetComponent<BoxCollider>();
        gameManager = GameManager.Instance;
        bulletSpawner = GetComponent<BulletSpawner>();
        firing = true;
    }

    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + weaponInfo.fireRate;
            InitializeProjectile();
        }
    }

    public void InitializeProjectile()
    {
        bulletSpawner.InitializeBullet(transform.position);
    }

    public void PickedUpNewWeapon()
    {
        pickedUp = true;
        boxCollider.enabled = false;
        OnPickedUp?.Invoke();
    }

    public void GameStarted(int value)
    {
        gameStarted = true;      
    }

    public void LevelCompleted()
    {
        firing = false;
    }

    public void AllowToShoot()
    {
        if (!firing)
        {
            return;
        }

        if (gameStarted)
        {
            if (pickedUp)
            {
                Fire();
            }
        }
    }

    public void SubscribeEvent()
    {
        gameManager.OnGameStarted += GameStarted;
        gameManager.OnLevelCompleted += LevelCompleted;
    }

    public void UnsubscribeEvent()
    {
        gameManager.OnGameStarted -= GameStarted;
        gameManager.OnLevelCompleted -= LevelCompleted;
    }
}