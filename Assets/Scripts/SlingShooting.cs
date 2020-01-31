using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShooting : MonoBehaviour
{


    public bool primary;
    private bool chosen;

    public float maxDistance;
    public float strength;

    private GameObject slingObject;
    public GameObject line;
    public GameObject supportLine;
    public float maxWidth=0.005f;
    public float minWidth=0.001f;

    public ComplexSling cs;
    private Shooting shooting;

    // Start is called before the first frame update
    void Start()
    {
        //this.GetComponent<Rigidbody>().AddForce(new Vector3(0,0,2));
        shooting = cs.gameObject.GetComponent<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting.menuInteraction) return;
        if(slingObject!= null)
        {
            if (primary)
            {
                if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)/*||Input.GetKeyDown(KeyCode.A)*/)
                {
                    chosen = true;
                    line.SetActive(true);
                    line.GetComponent<LineRenderer>().SetPosition(0, slingObject.transform.position);
                    slingObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    supportLine.SetActive(true);
                    supportLine.GetComponent<LineRenderer>().SetPosition(0, slingObject.transform.position);

                    //this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -3));
                }

                if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger)/* || Input.GetKeyUp(KeyCode.A)*/)
                {
                    slingObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    Vector3 direction = (slingObject.transform.position - this.transform.position);
                    //if distance is too much, use max distance, but in the right direction
                    direction = Vector3.ClampMagnitude(direction, maxDistance);

                    //need to understand if it is cool to have a "spring effect"
                    slingObject.GetComponent<Rigidbody>().AddForce(direction * strength);
                    VibrationManager.Instance.Shoot(true);
                    line.SetActive(false);
                    supportLine.SetActive(false);
                    chosen = false;
                    slingObject = null;
                }
            }
            else
            {
                if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger)/*||Input.GetKeyDown(KeyCode.A)*/)
                {
                    chosen = true;
                    line.SetActive(true);
                    line.GetComponent<LineRenderer>().SetPosition(0, slingObject.transform.position);
                    slingObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    supportLine.SetActive(true);
                    supportLine.GetComponent<LineRenderer>().SetPosition(0, slingObject.transform.position);

                    //this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -3));
                }
                if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger)/* || Input.GetKeyUp(KeyCode.A)*/)
                {
                    slingObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    Vector3 direction = (slingObject.transform.position - this.transform.position);
                    //if distance is too much, use max distance, but in the right direction
                    direction = Vector3.ClampMagnitude(direction, maxDistance);

                    //need to understand if it is cool to have a "spring effect"

                    slingObject.GetComponent<Rigidbody>().AddForce(direction * strength);
                    VibrationManager.Instance.Shoot(false);
                    line.SetActive(false);
                    supportLine.SetActive(false);
                    chosen = false;
                    slingObject = null;
                }
            }

        }
        
        if (chosen)
        {
            Vector3 direction = (slingObject.transform.position - this.transform.position);
            //if distance is too much, use max distance, but in the right direction
            direction = Vector3.ClampMagnitude(direction, maxDistance);

            line.GetComponent<LineRenderer>().SetPosition(1, slingObject.transform.position-direction);
            float normalizedDist = Vector3.Distance(slingObject.transform.position, this.transform.position) / maxDistance;
            supportLine.GetComponent<LineRenderer>().SetPosition(1, slingObject.transform.position + direction*2);
            line.GetComponent<LineRenderer>().startWidth = Mathf.Lerp(maxWidth,minWidth,normalizedDist);

            cs.enabled = false;
        }
        else
        {
            cs.enabled = true;
        }
    }


    public void OnTriggerEnter(Collider collision)
    {
        if (!chosen)
        {
            slingObject = collision.gameObject;
            VibrationManager.Instance.Inside(primary);
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (!chosen)
        {
            slingObject = null;
        }
    }
}
