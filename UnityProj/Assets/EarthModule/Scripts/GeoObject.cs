using UnityEngine;

public class GeoObject : MonoBehaviour
{
    public float Latitude;
    public float Longitude;
    public Transform Origin;

    // Update is called once per frame
    void Update()
    {
        //var position = Origin.position + /*Quaternion.Euler(new Vector3(Latitude, Longitude)) **/ Origin.forward * 20f;
        //var localPos = Quaternion.Euler(new Vector3(-Latitude, -Longitude)) * new Vector3(0, 0, 20);

        //transform.localPosition = localPos;

        //transform.position = position.RotateAround(Origin.position, new Vector3(Latitude, Longitude));
        SetCoordinates(Latitude, Longitude);
        //transform.position = position;
        //transform.RotateAround(Origin.position, new Vector3(1, 0, 0), Latitude);
        //transform.RotateAround(Origin.position, new Vector3(0, 1, 0), Longitude);
    }

    public void SetCoordinates(double latitude, double longitude)
    {
        var lat = Mathf.Clamp((float) latitude, -90, 90);
        var lon = Mathf.Clamp((float) longitude, -180, 180);

        var localPos = Quaternion.Euler(new Vector3(-lat, -lon)) * new Vector3(0, 0, 20);

        transform.localPosition = localPos;
    }
}
