using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapSlave : MonoBehaviour
{
    GameObject text;

    private void Start()
    {
        text = GameObject.FindGameObjectWithTag("DebugText");
    }

    private void OnTriggerStay(Collider other)
    {
        if (!this.gameObject.transform.root.GetComponent<OVRGrabbable>().isGrabbed) 
        {
            
            if (other.gameObject.tag == "SnapMaster")
            {
                //text.GetComponent<Text>().text += this.gameObject.transform.root.name + " is grabbed and is colliding with " + other.gameObject.name;
                //text.GetComponent<Text>().text += "SnapMaster";

                if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger) || OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
                {
                    this.transform.root.position = other.transform.position;
                    this.transform.root.gameObject.GetComponent<Rigidbody>().useGravity = false;
                }
            }
        }
        
    }
}
