using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool poolHole;
    private AudioSource myAs;

    public void Start()
    {
        myAs = GetComponent<AudioSource>();
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (!collision.gameObject.GetComponent<Attractor>().whiteBall)
            {
                Destroy(collision.gameObject);
            }

            if (myAs != null)
            {
                myAs.Play();
            }

            if (!poolHole)
            {
                LevelManager.Instance.HitTrigger(this.gameObject);
                Invoke("Rip", 0.2f);

            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //this gameobject is a trigger in the Space Trip Levels
        if (other.gameObject.layer != 9) return;
        TripLevelManager.Instance.HitTrigger(this.gameObject);
        other.gameObject.GetComponent<AudioSource>().Play();
        Destroy(this.gameObject);
    }
    private void Rip()
    {
        Destroy(this.gameObject);
    }
}
