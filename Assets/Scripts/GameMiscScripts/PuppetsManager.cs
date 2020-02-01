using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetsManager : MonoBehaviour
{
    private GameObject currentPuppet;
    public Puppet puppet;
    private GameObject[] puppets = new GameObject[ 10 ];

    void Start()
    {
        for ( int i = 1; i < 11; i++ )
        {
            puppets[ i - 1 ] = (GameObject)(Resources.Load( "Prefabs/Puppet" + i.ToString() ));
        }
        puppet = currentPuppet.GetComponent<Puppet>();
    }

    void Update()
    {

    }

    public bool CheckPuppet()
    {
        if ( puppet.CheckAllRight() )
            return true;
        else
            return false;
    }

}
