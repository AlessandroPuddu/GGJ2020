using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceBehaviour : MonoBehaviour
{
    public GameObject furnaceTop;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            furnaceTop.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            furnaceTop.SetActive(true);
        }
    }
}
