using UnityEngine;

public class EarthSpin : MonoBehaviour
{
    [SerializeField]
    private float _acceleration = 1.0f;
    
    public float Acceleration { get { return _acceleration; } set { _acceleration = value; } }

    private void Update()
    {
        var daytimeInSeconds = 24 * 60 * 60;

        var clockwiseAxis = Vector3.down;

        transform.Rotate(clockwiseAxis, Acceleration * 360 / daytimeInSeconds * Time.deltaTime, Space.Self);
    }
}
