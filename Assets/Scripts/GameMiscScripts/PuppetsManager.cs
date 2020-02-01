using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetsManager : MonoBehaviour
{
    public GameObject currentPuppet;
    public Puppet puppet;
    public GameObject[] puppets = new GameObject[ 10 ];
    private BodyPartsSpawner bodyPartsSpawner;

    private void Awake()
    {
        bodyPartsSpawner = GameObject.Find("BodyPartsSpawner").GetComponent<BodyPartsSpawner>();

        for (int i = 1; i < 11; i++)
        {
            puppets[i - 1] = (GameObject)(Resources.Load("Prefabs/Puppet" + i.ToString()));
        }
    }

    public void SetCurrentPuppet(GameObject go)
    {
        currentPuppet = go;
        puppet = currentPuppet.GetComponent<Puppet>();
        bodyPartsSpawner.NewPuppet(go);
    }

    public bool CheckPuppet()
    {
        if ( puppet.CheckAllRight() )
            return true;
        else
            return false;
    }
}
