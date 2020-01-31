using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attractor : MonoBehaviour
{

    public float G = 0.6674f;

    public static List<Attractor> Attractors;

    public bool whiteBall = false;
    private Vector3 resetPosition;

    public bool notAttracting = false;
    public float timeToLive;
    public bool planetShot = false;
    private float startTime;
    private Rigidbody rb;
    private AudioSource myAs;

    public AudioClip BumpWall;
    public AudioClip BumpEachOther;

    public bool spaceShip = false;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        startTime = Time.time;
        myAs = GetComponent<AudioSource>();

        resetPosition = this.transform.position;

        if (planetShot)
        {
            Invoke("unShot", 10f);
        }

        if (timeToLive != 0)
        {
            Invoke("destroyThis", timeToLive);
        }
    }

    public void unShot()
    {
        planetShot = false;
    }

    public void destroyThis()
    {
        Destroy(this.gameObject);
    }

    void FixedUpdate()
    {
        if (notAttracting)
        {
            return;
        }

        foreach (Attractor attractor in Attractors)
        {
            if (attractor != this)
                Attract(attractor);
        }
    }

    void OnEnable()
    {
        if (Attractors == null)
            Attractors = new List<Attractor>();

        Attractors.Add(this);
    }

    void OnDisable()
    {
        Attractors.Remove(this);
    }

    void Attract(Attractor objToAttract)
    {

        if(objToAttract.notAttracting && this.planetShot)
        {
            return;
        }
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position;
        direction-= rbToAttract.position;
        float sqrDistance = direction.sqrMagnitude;

        if (sqrDistance <= 0.001f)
            return;

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / sqrDistance;
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (whiteBall)
            {
                this.transform.position = resetPosition;
                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
            }
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (myAs != null)
            {
                myAs.clip = BumpWall;
                myAs.Play();
            }
        }
        if(collision.gameObject.layer == 9)
        {
            if (myAs != null)
            {
                myAs.clip = BumpEachOther;
                myAs.Play();
            }
        }
    }

}