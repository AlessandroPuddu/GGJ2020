using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform endPoint;
    private bool run;
    public float distance;

    // Update is called once per frame
    void Update()
    {
        if (endPoint && run == true)
        {
            distance = Vector3.Distance(transform.position, endPoint.position);
            if (distance > 0.005f)
            {
                transform.Translate(Vector3.right * Time.deltaTime);
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
