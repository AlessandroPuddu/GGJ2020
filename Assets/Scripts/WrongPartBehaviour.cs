using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OVRGrabbable))]
[RequireComponent(typeof(Rigidbody))]
public class WrongPartBehaviour : MonoBehaviour
{
    //[SerializeField]
    private OVRGrabbable oVR;
    //[SerializeField]
    private Rigidbody rb;
    //[SerializeField]
    //private BoxCollider bc;

    private Coroutine waitCR;

    float m_deltaTime = 0f;

    //[SerializeField]
    private Collider bc;

    private Vector3 startPosition = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;

    private Puppet puppet;

    private void Awake()
    {

        oVR = this.GetComponent<OVRGrabbable>();
        rb = this.GetComponent<Rigidbody>();
        bc = this.GetComponent<BoxCollider>();
        //rb = this.transform.root.gameObject.GetComponent<Rigidbody>();
        //puppet = GameObject.FindGameObjectWithTag( "SnapMaster" ).GetComponent<Puppet>();
        puppet = this.transform.root.gameObject.GetComponent<Puppet>();
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

            if(puppet != null) {
                // TODO change puppet bool also here for stuff that you have to remove (clown hair or nose...)
                puppet.ChangePuppetBool(gameObject.name);

            }

            if (waitCR == null) { 
                waitCR = StartCoroutine(WaitForEndGrab());    
            }
        }
    }

    private IEnumerator WaitForEndGrab() {
        startPosition = this.gameObject.transform.root.position;

        while (!rb.useGravity) {
            m_deltaTime += Time.deltaTime;
            
            if (!oVR.isGrabbed) { 
                bc.isTrigger = false;
                rb.isKinematic = false;
                rb.useGravity = true;
            }
            else {
                yield return null;
            }
        }

        endPosition = this.gameObject.transform.root.position;

        OVRInput.Controller controller = this.transform.root.GetComponent<OVRGrabbable>().grabbedBy.controller;
        OVRGrabber grabber = this.transform.root.GetComponent<OVRGrabbable>().grabbedBy;

        OVRPose localPose = new OVRPose { position = OVRInput.GetLocalControllerPosition(controller), orientation = OVRInput.GetLocalControllerRotation(controller) };
        OVRPose offsetPose = new OVRPose { position = grabber.M_anchorOffsetPosition, orientation = grabber.M_anchorOffsetRotation };
        localPose = localPose * offsetPose;

        OVRPose trackingSpace = transform.ToOVRPose() * localPose.Inverse();
        Vector3 linearVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerVelocity(controller);
        Vector3 angularVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerAngularVelocity(controller);

        rb.velocity = linearVelocity;
        rb.angularVelocity = angularVelocity;

        waitCR = null;
    }

    private Vector3 CalculateVelocity() {
        if(endPosition == Vector3.zero) { 
            endPosition = startPosition;    
        }

        float dx = endPosition.x - startPosition.x;
        float dy = endPosition.y - startPosition.y;
        float dz = endPosition.z - startPosition.z;
        
        float d = Mathf.Sqrt(dx*dx + dy*dy + dz * dz);

        float vx = dx/d * d/m_deltaTime;
        float vy = dy/d * d/m_deltaTime;
        float vz = dz/d * d/m_deltaTime;

        return new Vector3(vx,vy,vz);
    }
}
