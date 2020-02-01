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
            transform.position = Vector3.Lerp(transform.position, endPoint.position, 0.9f * Time.deltaTime);
            if (Vector3.Distance(transform.position, endPoint.position) < 0.01f)
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
