using UnityEngine;

public class LerpMovement : MonoBehaviour
{
   public static LerpMovement Instance;     

    private float elapsedTime;

    private void OnEnable() 
    {
        InitializeSingleton();
    }

    private void InitializeSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SmoothLerpingTransform(Vector3 startPosition, Vector3 endPosition, float desiredDuration) 
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredDuration;

        transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);
    }
}
