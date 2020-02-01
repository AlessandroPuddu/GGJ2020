using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayRaycast : MonoBehaviour
{
    private OVRGrabbable root;
    private OVRGrabber currentGrabber;
    [SerializeField]
    private ParticleSystem sprayParticle;
    [SerializeField]
    private GameObject particleParent;

    private Material rootMaterial;

    private void Start()
    {
        root = this.transform.root.gameObject.GetComponent<OVRGrabbable>();
        rootMaterial = this.transform.root.gameObject.GetComponent<Renderer>().material;
        //sprayParticle.Stop();
        particleParent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(root != null) {
            if (root.isGrabbed)
            {
                // Check if grabber is changed
                if (currentGrabber != root.grabbedBy)
                {
                    currentGrabber = root.grabbedBy;
                }
                // If grabber is left controller
                if (currentGrabber.controller == OVRInput.Controller.LTouch)
                {
                    rootMaterial.color = Color.red;
                    // We listen to left trigger input
                    if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) > 0)
                    {
                        rootMaterial.color = Color.yellow;
                        
                        //if(!sprayParticle.isPlaying)
                            //sprayParticle.Play();
                        if(!particleParent.activeInHierarchy)
                            particleParent.SetActive(true);

                        ManageHit();
                    }
                    else {
                        //if (sprayParticle.isPlaying) { 
                        //sprayParticle.Pause();
                        if (particleParent.activeInHierarchy) { 
                            particleParent.SetActive(false);
                        }    
                    }
                }
                // If grabber is right controller
                if (currentGrabber.controller == OVRInput.Controller.RTouch)
                {
                    rootMaterial.color = Color.blue;
                    // We check fot right trigger input
                    if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) > 0)
                    {
                        rootMaterial.color = Color.green;

                        //if (!sprayParticle.isPlaying)
                        //sprayParticle.Play();
                        if (!particleParent.activeInHierarchy)
                            particleParent.SetActive(true);

                        ManageHit();
                    }
                    else {
                        //if (sprayParticle.isPlaying)
                        //{
                        if (particleParent.activeInHierarchy) { 
                            particleParent.SetActive(false);
                            //sprayParticle.Pause();
                        }
                    }
                }
            }
        }
    }
    private void ManageHit()
    {
        RaycastHit objectHit;

        //Vector3 fwd = this.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(this.transform.position, this.transform.forward * 50, Color.green);

        if (Physics.Raycast(this.transform.position, this.transform.forward, out objectHit, 50))
        {
            //objectHit.collider.gameObject.transform.root.gameObject.GetComponent<Renderer>().material.color = Color.red;
            ToColor toColor = objectHit.collider.gameObject.transform.root.gameObject.GetComponent<ToColor>();

            if(toColor != null) { 
                toColor.NotifyHitInThisFrame();    
            }
        }
    }
}

