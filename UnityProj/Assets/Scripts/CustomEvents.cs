using System;
using Assets.Core.Scripts;
using Assets.Core.Scripts.Dtos;
using UnityEngine;
using UnityEngine.Events;


namespace Assets.Scripts
{
    [Serializable]
    public class IntegerEvent : UnityEvent<int>
    {
    }

    [Serializable]
    public class GroundStationSelectedEvent : UnityEvent<GroundStation>
    {
    }

    [Serializable]
    public class SatellitedSelectEvent : UnityEvent<Satellite>
    {
    }
}