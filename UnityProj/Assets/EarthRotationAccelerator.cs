using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EarthRotationAccelerator : MonoBehaviour
{
    public int[] Accelerations;

    public TMP_Text CurrentAccelerationText;

    public IntegerEvent AccelerationChanged;

    private int _currentAccelerationIndex;

    public void EncreaseAcceleration()
    {
        if(_currentAccelerationIndex + 1 < Accelerations.Length)
        {
            _currentAccelerationIndex++;

            var newAcceleration = Accelerations[_currentAccelerationIndex];

            CurrentAccelerationText.text = "x" + newAcceleration.ToString();
            AccelerationChanged.Invoke(newAcceleration);
        }
        else
        {
            Debug.LogWarning("Its maximal acceleration");
        }
    }

    public void DecreaseAcceleration()
    {
        if (_currentAccelerationIndex - 1 > 0)
        {
            _currentAccelerationIndex--;

            var newAcceleration = Accelerations[_currentAccelerationIndex];

            CurrentAccelerationText.text = "x" + newAcceleration.ToString();
            AccelerationChanged.Invoke(newAcceleration);
        }
        else
        {
            Debug.LogWarning("Its minimal acceleration");
        }
    }
}

[Serializable]
public class IntegerEvent : UnityEvent<int>
{

}
