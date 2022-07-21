using UnityEngine;

public class AUG : Weapon
{
    private void Start()
    {
        InitializeModel();
        InitializeVariables();
        SubscribeEvent();
    }

    private void OnDisable() 
    {
        UnsubscribeEvent();   
    }

    private void Update() 
    {
        AllowToShoot();
    }
}
