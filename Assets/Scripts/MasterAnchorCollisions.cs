using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterAnchorCollisions : MonoBehaviour
{
    [SerializeField]
    private GameObject inactiveChild;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SnapSlave")
        {

            OVRGrabbable oVR = other.transform.root.gameObject.GetComponent<OVRGrabbable>();

            if (oVR != null)
            {
                if (!oVR.isGrabbed)
                {
                    Destroy(other.transform.root.gameObject);

                    // TODO - Check if this is already active?
                    inactiveChild.SetActive(true);
                }
            }
        }
    }
}
