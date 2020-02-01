using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterAnchorCollisions : MonoBehaviour
{
    [SerializeField]
    private GameObject inactiveChild;

    [SerializeField]
    private GameObject rightPart;

    // TODO - On trigger stay
    // hiltight this to give feedback
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SnapSlave")
        {

            OVRGrabbable oVR = other.transform.root.gameObject.GetComponent<OVRGrabbable>();

            if (oVR != null)
            {
                if (!oVR.isGrabbed)
                {
                    //oVR.grabbedBy.ForceRelease(oVR);
                    // TODO - maybe destroy ovr.go?
                    Destroy(oVR.gameObject);
                    
                    // TODO - Check if this is already active?
                    // TODO - Create a copy and then set active?
                    inactiveChild.SetActive(true);

                    //GameEvents.RaiseOnRightPartPlaced();
                }
            }
        }
    }

    public void AssignRightPart(GameObject part) { 
        this.rightPart = part;
    }
}
