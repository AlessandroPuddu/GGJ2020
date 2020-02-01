using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveChildsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inactiveChild;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SnapSlave") { 
            
            OVRGrabbable oVR = other.transform.root.gameObject.GetComponent<OVRGrabbable>();

            if(oVR != null) {
                if (!oVR.isGrabbed) { 
                    Destroy(other.gameObject);
                    inactiveChild.SetActive(true);
                }   
            }    
        }
    }
}
