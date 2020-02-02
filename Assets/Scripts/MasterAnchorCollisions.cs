using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MasterAnchorCollisions : MonoBehaviour
{
    //[SerializeField]
    private GameObject inactiveChild;

    //[SerializeField]
    //private GameObject rightPart;

    private Puppet puppet;

    private void Start()
    {
        inactiveChild = this.transform.parent.gameObject;
        puppet = transform.root.gameObject.GetComponent<Puppet>();
    }

    // TODO - On trigger stay
    // hiltight this to give feedback
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SnapSlave")
        {
            
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    
            StartCoroutine(Destroy(other.gameObject));        

            // TODO - Check if this is already active?
            // TODO - Create a copy and then set active?
            //inactiveChild.SetActive(true);
            inactiveChild.GetComponent<MeshRenderer>().enabled = true;
            // Changes the boolean in puppet to being fixed for the specific body part
            

            string name = other.gameObject.name;

            if (name.Contains("(Clone)")){ 
                int founds1 = name.IndexOf("(");
                name = name.Remove(founds1);
            }

            puppet.ChangePuppetBool( name );
        }
    }

    //public void AssignRightPart(GameObject part) { 
    //    this.rightPart = part;
    //}

    private IEnumerator Destroy(GameObject gameobject) { 
        yield return new WaitForSeconds(5.0f);
        
        Destroy(gameobject);
    }
}
