using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class EarthInteractionHandler : MonoBehaviour
{
    public SatelitesWorldSpaceLoader SatelitesManager;
    public EarthRotationAccelerator EarthRotationAccelerator;
    public EarthSpin EarthSpin;
    private GameObject _touchPoint;

    private void Start()
    {
#if !UNITY_EDITOR
        gameObject.AddComponent<TouchHandler>().OnTouchCompleted.AddListener(HandleTouch);
#else
        gameObject.AddComponent<PointerHandler>().OnPointerClicked.AddListener(HandleClick);
#endif

        EarthRotationAccelerator.AccelerationChanged.AddListener(accel => EarthSpin.Acceleration = accel);
    }


    private void HandleTouch(HandTrackingInputEventData eventData)
    {
        CreateTouchPoint(eventData.InputData);
        SatelitesManager.ShowSatelitesInPoint(eventData.InputData);
    }

    private void HandleClick(MixedRealityPointerEventData eventData)
    {
        if (eventData.Pointer.Result != null)
        {
            var hitObject = eventData.Pointer.Result.Details.Object;
            if (hitObject)
            {
                CreateTouchPoint(eventData.Pointer.Result.Details.Point);
                SatelitesManager.ShowSatelitesInPoint(eventData.Pointer.Result.Details.Point);
            }
        }
    }

    private void CreateTouchPoint(Vector3 position)
    {
        if (_touchPoint != null)
        {
            Destroy(_touchPoint);
            _touchPoint = null;
        }

        _touchPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _touchPoint.transform.localScale = new Vector3(0.1f, 0.1f, 0.08f);
        _touchPoint.GetComponent<MeshRenderer>().material.color = Color.yellow;
        _touchPoint.name = "TouchPoint";
        _touchPoint.transform.position = position;
    }
}
