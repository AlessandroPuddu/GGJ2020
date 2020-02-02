using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helpOrientation : MonoBehaviour
{

    public OVRGrabbable ovrG;
    // Update is called once per frame

    private void Start()
    {
        ovrG = GetComponent<OVRGrabbable>();
    }
    void Update()
    {
        if (ovrG.snapOrientation)
        {

        }
    }
}
