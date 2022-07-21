using UnityEngine;

public abstract class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    public abstract void InitializeSingleton();

    public abstract void InitializeVariables();
}
