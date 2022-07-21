using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPattern : MonoBehaviour
{
    public enum Pattern
    {
        Single,
        Triple,
    };

    [SerializeField] private Pattern pattern;

    private void OnEnable()
    {
        GetBulletPattern();
    }

    private void GetBulletPattern()
    {
        if (pattern == Pattern.Single)
        {

            
        }
        else if (pattern == Pattern.Triple)
        {

        }
    }
}
