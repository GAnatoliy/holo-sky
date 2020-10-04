using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Ellipse : MonoBehaviour
{
    public float a = 5;
    public float b = 3;
    public float Cx = 0;
    public float Cy = 0;
    public float Cz = 0;
    public float x = 0;
    public float y = 0;
    public float z = 45;
    public int resolution = 1000;
    private Vector3[] positions;

    void Update()
    {
        positions = CreateEllipse(a, b, Cx, Cy, Cz, z, resolution);
        var lr = GetComponent<LineRenderer>();
        lr.positionCount = (resolution + 1);
        for (int i = 0; i <= resolution; i++) {
            lr.SetPosition(i, positions[i]);
        }
    }

    Vector3[] CreateEllipse(float a, float b, float Cx, float Cy, float Cz, float theta, int resolution)
    {
        positions = new Vector3[resolution + 1];
        var qX = Quaternion.AngleAxis(90 + x, Vector3.right);
        var qZ = Quaternion.AngleAxis(theta, Vector3.forward);
        var qY = Quaternion.AngleAxis(y, Vector3.up);
        var center = new Vector3(Cx, Cy, Cz);
        for (int i = 0; i <= resolution; i++)
        {
            float angle = (float)i / (float)resolution * 2.0f * Mathf.PI;
            positions[i] = /*Quaternion.Euler(new Vector3(90, 0, 0)) * */new Vector3(a * Mathf.Cos(angle), b * Mathf.Sin(angle), 0.0f);
            positions[i] = qX * qY * qZ * positions[i] + center;
        }
        return positions;
    }
}