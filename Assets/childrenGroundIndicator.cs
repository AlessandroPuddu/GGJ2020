using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childrenGroundIndicator : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector3(this.transform.position.x, -0.781f, this.transform.position.z);   
    }
}
