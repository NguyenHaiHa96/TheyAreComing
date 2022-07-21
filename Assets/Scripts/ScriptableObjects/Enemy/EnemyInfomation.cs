using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Scriptable Objects/Enemy")]
public class EnemyInfomation : ScriptableObject
{
    [Header("Enemy Name")]
    public new string name;

    [Header("Enemy Stats")]
    public float radiusCheck;
    public int health;
    public int moveSpeed;
    public int chaseSpeed;

    [Header("Enemy Model")]
    public GameObject model;
}
