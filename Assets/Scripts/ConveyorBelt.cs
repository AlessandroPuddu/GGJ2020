using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public Transform spawnPoint;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsConveyorBeltMoving())
        {
            transform.Translate(Vector3.down * Time.deltaTime * 1.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "ObjectsKiller")
        {
            transform.position = spawnPoint.position;
        }
    }
}
