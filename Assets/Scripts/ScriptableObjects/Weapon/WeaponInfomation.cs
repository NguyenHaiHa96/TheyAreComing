using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Objects/Weapon")]
public class WeaponInfomation : ScriptableObject
{
    [Header ("Gun Name")]
    public new string name;

    [Header("Gun Stats")]
    public int damage;
    public float fireRate;
    public float BulletMoveSpeed;

    [Header("Model")]
    public GameObject gunModel;
    public GameObject bulletModel;
}
