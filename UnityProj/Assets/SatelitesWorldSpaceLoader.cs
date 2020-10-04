using Assets.Core.Scripts;
using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SatelitesWorldSpaceLoader : MonoBehaviour
{ 
    public GameObject SateliteWorldPrefab;
    public Transform Origin;
    public Transform SceneContainer;

    public const float EARTH_RADIUS_IN_METERS = 6378137;

    private List<GameObject> _satelites = new List<GameObject>();

    private Dictionary<string, SatelliteObject> _statellits = new Dictionary<string, SatelliteObject>();

    private SatellitedSelectEvent _sputnikSelectedEvent = new SatellitedSelectEvent();

    public void ShowSatelitesInPoint(Vector3 point)
    {
        RemoveAllSatelites();

        var local = Origin.transform.InverseTransformPoint(point);
        var latLon = ToSpherical(local);
        var sphericalGeocoordinate = new GeoCoordinate(latLon.x, latLon.y);

        var gsProvider = new DataObjectsProvider();
        var satelites = gsProvider.GetSatellites().Where(sat => sat.IsVisibleFromPointNow(sphericalGeocoordinate)).Take(3).ToArray();

        var cameraMain = Camera.main.transform;

        var positions = new Vector3[3] {
            cameraMain.forward + new Vector3(40f, 7f, 30f),
            cameraMain.forward + new Vector3(25f, 7f, 30f),
            cameraMain.forward + new Vector3(5f, 7f, 20f),
        };

        for (var i = 0; i < satelites.Length; i++) {
            var worldSatelite = CreateWorldSatelite($"{satelites[i].ObjectName}");
            worldSatelite.transform.position = positions[i];


            var satellite = worldSatelite.AddComponent<SatelliteObject>();
            satellite.Init(satelites[i]);
            satellite.OnStationSelected(OnSatelliteSelected);

            _statellits.Add(satelites[i].ObjectId, satellite);
        }
    }

    private void OnSatelliteSelected(Satellite satellite)
    {
        _sputnikSelectedEvent.Invoke(satellite);
    }

    public void SatelliteSelected(UnityAction<Satellite> handler)
    {
        _sputnikSelectedEvent.AddListener(handler);
    }

    private GameObject CreateWorldSatelite(string name)
    {
        var satelite = Instantiate(SateliteWorldPrefab);
        satelite.transform.SetParent(SceneContainer, false);
        satelite.name = !string.IsNullOrEmpty(name) ? name + "_World" : "Satelite_World";
        _satelites.Add(satelite);
        return satelite;
    }



    private void RemoveAllSatelites()
    {
        foreach(var sat in _satelites)
        {
            Destroy(sat);
        }

        _satelites.Clear();
    }


    //https://gamedev.stackexchange.com/questions/149109/calculating-the-latitude-longitude-of-a-raycast-hit-point-on-a-sphere
    public static Vector2 ToSpherical(Vector3 position)
    {
        // Convert to a unit vector so our y coordinate is in the range -1...1.
        position = Vector3.Normalize(position);

        // The vertical coordinate (y) varies as the sine of latitude, not the cosine.
        float lat = Mathf.Asin(position.y) * Mathf.Rad2Deg;

        // Use the 2-argument arctangent, which will correctly handle all four quadrants.
        float lon = Mathf.Atan2(position.x, position.z) * Mathf.Rad2Deg;

        // I usually put longitude first because I associate vector.x with "horizontal."
        return new Vector2(lat, -lon);
    }
}