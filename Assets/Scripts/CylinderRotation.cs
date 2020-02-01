using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderRotation : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.IsConveyorBeltMoving())
        {
            transform.Rotate(new Vector3(0,0,100f) * Time.deltaTime, Space.Self);
        }
    }
}
