    using UnityEngine;

public class AK47 : Weapon
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
