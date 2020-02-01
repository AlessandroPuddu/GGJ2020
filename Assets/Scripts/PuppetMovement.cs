using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform endPoint;
    private bool run;
    private float distance;

    // Update is called once per frame
    void Update()
    {
        if (endPoint && run == true)
        {
            if (transform.position != endPoint.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPoint.position, 1.5f * Time.deltaTime);
            } else
            {
                run = false;
                GameManager.Instance.SetConveyorBeltMovement(run);
            }
        }
    }

    public void SetEndpoint(Transform endpoint)
    {
        this.endPoint = endpoint;
        run = true;
        GameManager.Instance.SetConveyorBeltMovement(run);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "ObjectsKiller")
        {
            GameManager.Instance.PuppetCheck(gameObject);
            Destroy(gameObject);
            run = false;
            GameManager.Instance.SetConveyorBeltMovement(run);
        }
    }
 }
