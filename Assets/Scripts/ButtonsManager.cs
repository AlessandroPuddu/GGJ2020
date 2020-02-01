using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter( Collider other )
    {
        if (other.gameObject.layer == gameObject.layer)
        {
            // activate
        }
    }
}
