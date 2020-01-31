using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleeping : MonoBehaviour
{

    public float time= 1f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Shooting>().enabled = false;
        Invoke("SleepEnd", time);
    }

    // Update is called once per frame
    void SleepEnd()
    {
        GetComponent<Shooting>().enabled = true;
    }
}
