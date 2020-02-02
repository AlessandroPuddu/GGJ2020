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

    //[SerializeField]
    //private BoxCollider sprayCollider;

    private Material rootMaterial;

    private AudioSource myAS;

    private bool hitThisFrame = false;

    private void Start()
    {
        root = this.transform.root.gameObject.GetComponent<OVRGrabbable>();
        rootMaterial = this.transform.root.gameObject.GetComponent<Renderer>().material;
        //sprayParticle.Stop();
        particleParent.SetActive(false);
        myAS= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //sprayCollider.enabled = false;

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

                        if (!myAS.isPlaying) myAS.Play();

                            //if(!sprayParticle.isPlaying)
                            //sprayParticle.Play();
                            if (!particleParent.activeInHierarchy) {
                                particleParent.SetActive(true);
                                sprayParticle.Play();
                            }
                            

                        ManageHit();
                    }
                    else {
                        // if (sprayParticle.isPlaying) { 
                        // sprayParticle.Pause();
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
                        if (!myAS.isPlaying) myAS.Play();
                        //if (!sprayParticle.isPlaying)
                        //sprayParticle.Play();
                        if (!particleParent.activeInHierarchy)
                        {
                            particleParent.SetActive(true);
                            sprayParticle.Play();
                        }

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
                if(currentGrabber.controller == OVRInput.Controller.LTouch && OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
                {
                    //if (sprayParticle.isPlaying)
                    //sprayParticle.Pause();
                    myAS.Stop();
                }

                if (currentGrabber.controller == OVRInput.Controller.RTouch && OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
                {
                    //if (sprayParticle.isPlaying)
                    //sprayParticle.Pause();
                    myAS.Stop();
                }
                //TO DO INTERROMPERE FUORI GRABBING

            }
            else { 
                ManageHit();
            }
        }   
    }
    private void ManageHit()
    {
        RaycastHit objectHit;

        Vector3 fwd = this.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(this.transform.position,- this.transform.forward * 50, Color.green);

        LayerMask mask = 1 << 12;

        if (Physics.SphereCast(this.transform.position, 0.1f, - this.transform.forward, out objectHit, 50,mask))
        {
            //objectHit.collider.gameObject.transform.root.gameObject.GetComponent<Renderer>().material.color = Color.red;
            ToColor toColor = objectHit.collider.gameObject.GetComponent<ToColor>();

            Debug.Log(objectHit.collider.gameObject.name);

            if(toColor != null) { 
                toColor.NotifyHitInThisFrame();    
            }
        }
        //sprayCollider.enabled = true;
    }

    //public void HasToNotifyHit(GameObject targetHit) { 
    //    targetHit.transform.root.gameObject.GetComponent<ToColor>();
    //}
}

