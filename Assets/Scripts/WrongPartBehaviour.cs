using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OVRGrabbable))]
[RequireComponent(typeof(Rigidbody))]
public class WrongPartBehaviour : MonoBehaviour
{
    [SerializeField]
    private OVRGrabbable oVR;
    [SerializeField]
    private Rigidbody rb;
    //[SerializeField]
    //private BoxCollider bc;

    private Coroutine waitCR;

    [SerializeField]
    private Collider bc;

    private Puppet puppet;

    private void Awake()
    {
        oVR = this.GetComponent<OVRGrabbable>();
        //rb = this.transform.root.gameObject.GetComponent<Rigidbody>();
        puppet = GameObject.FindGameObjectWithTag( "SnapMaster" ).GetComponent<Puppet>();
    }

    private void Update()
    {
        if (oVR.isGrabbed) {
            if(!bc.isTrigger)
                bc.isTrigger = true;
            if(!rb.isKinematic)
                rb.isKinematic = true;
            if(rb.isKinematic)
                rb.useGravity = false;

            // TODO change puppet bool also here for stuff that you have to remove (clown hair or nose...)
            puppet.ChangePuppetBool( gameObject.name );

            if(waitCR == null) { 
                waitCR = StartCoroutine(WaitForEndGrab());    
            }
        }
    }

    private IEnumerator WaitForEndGrab() {
        while (!rb.useGravity) {
            if (!oVR.isGrabbed) { 
                bc.isTrigger = false;
                rb.isKinematic = false;
                rb.useGravity = true;
            }
            else {
                yield return null;
            }
        }
        waitCR = null;
    }
}
