using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartsSpawner : MonoBehaviour
{
    private GameObject armModel;
    private GameObject armInstance;
    private Vector3 spawnLocation = new Vector3( 0f, 5f, 0f );

    void Start()
    {
        armModel = Resources.Load( "Prefabs/Arm" ) as GameObject;
        armInstance = Instantiate( armModel, spawnLocation, new Quaternion() );
    }

    void Update()
    {
        
    }

    //public void SpawnBodyParts(string[] bodyParts)
    //{
    //    foreach (string s in bodyParts )
    //    {
    //        switch (s)

    //    }
    //}
}
