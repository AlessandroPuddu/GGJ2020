using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OVRGrabbable))]
[RequireComponent(typeof(Rigidbody))]
public class WrongPartBehaviour : MonoBehaviour
{
    private OVRGrabbable oVR;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private BoxCollider bc;

    private void Awake()
    {
        oVR = this.GetComponent<OVRGrabbable>();
        //rb = this.transform.root.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //if (rb.useGravity) { 
        //    Destroy(this);    
        //}

        if (oVR.isGrabbed && !rb.useGravity) {
            // If I grab the wrong part
            // I have to reenable its 
            // rigidbody
            bc.isTrigger = false;
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}
