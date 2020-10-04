using UnityEngine;

public class EllipseOrbitFactory : MonoBehaviour
{
    public Transform Container;
    public GameObject SatellitePrefab;

    // Start is called before the first frame update
    //void Start()
    //{
    //    var firstUnknNum = 97.4688f;
    //    var secondUnknNum = 350.8551f;
    //    var thirdUnknNum = 0.0010487f;
    //    var fourthUnknNum = 114.8171f;
    //    var fifthUnknNum = 245.4153f;
    //    var sixthUnknNum = 15.14531871f;

    //    var a = 6.6228f / Mathf.Pow(sixthUnknNum, 2.0f / 3.0f);
    //    var b = Mathf.Sqrt((-(thirdUnknNum* thirdUnknNum) +1) * a*a);
    //    Debug.LogWarning($"{a} - {b}");
    //    var metersPerUnit = SatelitesManager.EARTH_RADIUS_IN_METERS / 20.0f;

    //    a = a * SatelitesManager.EARTH_RADIUS_IN_METERS;
    //    b = b * SatelitesManager.EARTH_RADIUS_IN_METERS;

    //    Ellipse.CreateEllipse("1", Container, Container.position, a / metersPerUnit, b / metersPerUnit, firstUnknNum, secondUnknNum, 0, 800);
    //    Debug.Log($"Major axis {SatelitesManager.EARTH_RADIUS_IN_METERS * a}");
    //}

    public void CreateEllipseOrbit(float first, float second, float third, float fourth, float fifth, float sixth)
    {
        var a = 6.6228f / Mathf.Pow(sixth, 2.0f / 3.0f);
        var b = Mathf.Sqrt((-(third * third) + 1) * a * a);
        Debug.LogWarning($"{a} - {b}");
        var metersPerUnit = SatelitesManager.EARTH_RADIUS_IN_METERS / 20.0f;

        a = a * SatelitesManager.EARTH_RADIUS_IN_METERS;
        b = b * SatelitesManager.EARTH_RADIUS_IN_METERS;

        Ellipse.CreateEllipse("1", Container, Container.position, a / metersPerUnit, b / metersPerUnit, first, second, 0, 800);
    }
}
