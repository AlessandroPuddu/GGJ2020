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
    private Vector3 spawnLocation = new Vector3( 0f, 5f, 0f );

    // TBD
    private Vector3 spawnLocation1 = new Vector3( 0f, 5f, 1f );
    private Vector3 spawnLocation2 = new Vector3( 0f, 5f, -1f );
    private Vector3 spawnLocation3 = new Vector3( 1f, 5f, 0f );
    private Vector3 spawnLocation4 = new Vector3( -1f, 5f, 0f );
    private Vector3 spawnLocation5 = new Vector3( 1f, 5f, 1f );
    private Vector3[] spawnLocations = new Vector3[ 5 ];

    private string[] bodyPartsStrings = new string[ 5 ] { "RightArm", "LeftArm", "RightLeg", "LeftLeg", "Head" };

    void Start()
    {
        spawnLocations[ 0 ] = spawnLocation1;
        spawnLocations[ 1 ] = spawnLocation2;
        spawnLocations[ 2 ] = spawnLocation3;
        spawnLocations[ 3 ] = spawnLocation4;
        spawnLocations[ 4 ] = spawnLocation5;
        SpawnBodyParts( bodyPartsStrings );
    }

    void Update()
    {

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

        for (int i = 0; i < bodyPartsToSpawn.Length; i++ )
        {
            bodyPartsToSpawn[ i ] = Instantiate( bodyPartsToSpawn[ i ], spawnLocations[ i ], new Quaternion() );
        }
    }

}

