using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundIndicator : MonoBehaviour
{

    public GameObject toFollow;

    private Transform toFollowTransform;
    // Start is called before the first frame update
    void Start()
    {
        toFollowTransform = toFollow.transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(toFollowTransform.position.x,-0.781f,toFollowTransform.position.z);
    }
}
