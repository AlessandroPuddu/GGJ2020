using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartsSpawner : MonoBehaviour
{
    private GameObject bodyPartModel;
    private GameObject rightArm;
    private GameObject leftArm;
    private GameObject rightLeg;
    private GameObject leftLeg;
    private GameObject head;
    private Vector3 spawnLocation;
    private Puppet puppet;
    private PuppetsManager puppetsManager;

    private string[] bodyPartsStrings = new string[ 5 ] { "RightArm", "LeftArm", "RightLeg", "LeftLeg", "Head" };

    void Start()
    {
        puppetsManager = GameObject.FindGameObjectWithTag( "PuppetsManager" ).GetComponent<PuppetsManager>();
        puppet = puppetsManager.puppet;
        bodyPartsStrings[ 0 ] = puppet.GetPartToSpawn();
        spawnLocation = new Vector3( transform.position.x, transform.position.y, transform.position.z );
    }

    public void SpawnBodyParts( string[] bodyParts )
    {
        GameObject[] bodyPartsToSpawn = new GameObject[ bodyParts.Length ];
        for ( int i = 0; i < bodyParts.Length; i++ )
        {
            switch ( bodyParts[ i ] )
            {
                case "RightArm":
                    rightArm = Resources.Load( "Prefabs/RightArm" ) as GameObject;
                    bodyPartsToSpawn[ i ] = rightArm;
                    break;
                case "LeftArm":
                    leftArm = Resources.Load( "Prefabs/LeftArm" ) as GameObject;
                    bodyPartsToSpawn[ i ] = leftArm;
                    break;
                case "RightLeg":
                    rightLeg = Resources.Load( "Prefabs/RightLeg" ) as GameObject;
                    bodyPartsToSpawn[ i ] = rightLeg;
                    break;
                case "LeftLeg":
                    leftLeg = Resources.Load( "Prefabs/LeftLeg" ) as GameObject;
                    bodyPartsToSpawn[ i ] = leftLeg;
                    break;
                case "Head":
                    head = Resources.Load( "Prefabs/Head" ) as GameObject;
                    bodyPartsToSpawn[ i ] = head;
                    break;
            }
        }

        bodyPartsToSpawn[ 0 ] = Instantiate( bodyPartsToSpawn[ 0 ], spawnLocation, new Quaternion() );

        //for (int i = 0; i < bodyPartsToSpawn.Length; i++ )
        //{
        //    bodyPartsToSpawn[ i ] = Instantiate( bodyPartsToSpawn[ i ], spawnLocations[ i ], new Quaternion() );
        //}
    }

}

