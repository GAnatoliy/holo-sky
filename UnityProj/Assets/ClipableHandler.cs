using Assets.Scripts;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class ClipableHandler : MonoBehaviour
{
    public EllipseOrbitFactory OrbitFactory;


    public void OnClick()
    {
        var satelliteObject = GetComponent<SatelliteObject>();

        var model = satelliteObject._model;


        //OrbitFactory.CreateEllipseOrbit(Convert.ToSingle(model.Inclination), Convert.ToSingle(model.RaOfAscNode), Convert.ToSingle("0." + model.Eccentricity),
        //    Convert.ToSingle(model.Periapsis));
    }
}
