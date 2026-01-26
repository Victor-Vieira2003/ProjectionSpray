using UnityEngine;
using UnityEngine.Events;

public class Spray : MonoBehaviour
{
    public OVRGrabber rightHand;
    public OVRGrabber leftHand;
    public OVRGrabbable grabbable;

    public UnityEvent onGrab;
    public  UnityEvent onRelease;
    void Update()
    {
        bool isGrabbed = grabbable.isGrabbed;
        bool isRightHandDrawing = isGrabbed && grabbable.grabbedBy == rightHand && OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger);
        bool isLeftHandDrawing = isGrabbed && grabbable.grabbedBy == leftHand && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
        if (isRightHandDrawing || isLeftHandDrawing)
        {
            onGrab.Invoke();
        }
        else
        {
            onRelease.Invoke();
        }
    }
}
