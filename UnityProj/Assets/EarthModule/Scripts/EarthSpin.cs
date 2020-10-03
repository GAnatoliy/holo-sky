using UnityEngine;

public class EarthSpin : MonoBehaviour
{
    public float Acceleration = 1.0f;
    
    private void Update()
    {
        var daytimeInSeconds = 24 * 60 * 60;

        var clockwiseAxis = Vector3.down;

        transform.Rotate(clockwiseAxis, Acceleration * 360 / daytimeInSeconds * Time.deltaTime, Space.Self);
    }
}
