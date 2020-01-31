using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTripControls : MonoBehaviour
{
    public GameObject spaceShip;

    public GameObject primaryHand;
    public GameObject secondaryHand;
    public enum states { not_started, driving, boost, paused };
    states current_state = states.not_started;

    public GameObject laser;
    private LineRenderer line;
    public GameObject falsePlanet;

    //to define the direction
    private GameObject positiveHandDirection;
    private GameObject startingHandDirection;

    public float speedBasic = 2.0f;


    public void Awake()
    {
        spaceShip.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        line = laser.GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (current_state == states.driving)
        {
            drivingHandle();
        }
        if(current_state == states.not_started)
        {
            waitingFirstInput();
        }
        if (current_state == states.paused)
        {
            
        }
    }

    private void waitingFirstInput()
    {
        if (Input.GetKeyDown(KeyCode.A)||OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            current_state = states.driving;
            spaceShip.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            startingHandDirection = secondaryHand;
            positiveHandDirection = primaryHand;
            TripLevelManager.Instance.setUpAndStart();
            
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger)|| OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            current_state = states.driving;
            startingHandDirection = primaryHand;
            positiveHandDirection = secondaryHand;
            spaceShip.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            TripLevelManager.Instance.setUpAndStart();
        }
    }
    private void drivingHandle()
    {
        laser.SetActive(true);
        falsePlanet.SetActive(true);
        line.SetPosition(0, startingHandDirection.transform.position);
        line.SetPosition(1, positiveHandDirection.transform.position);

        falsePlanet.transform.position = positiveHandDirection.transform.position;

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            startingHandDirection = secondaryHand;
            positiveHandDirection = primaryHand;

        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            startingHandDirection = primaryHand;
            positiveHandDirection = secondaryHand;
        }

    }

    public void FixedUpdate()
    {
        if(current_state == states.driving)
        {
            Vector3 direction = (positiveHandDirection.transform.position - startingHandDirection.transform.position);
            spaceShip.GetComponent<Rigidbody>().velocity = (direction) * speedBasic;

            //spaceShip.transform.position = Vector3.Lerp(spaceShip.transform.position, spaceShip.transform.position + direction, Time.deltaTime * speedBasic);
            spaceShip.transform.LookAt(spaceShip.transform.position + direction.normalized);
        }
    }


    public void Pause(bool pause)
    {
        if (pause)
        {
            current_state = states.paused;
            laser.SetActive(false);
            falsePlanet.SetActive(false);
            spaceShip.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            current_state = states.driving;
            spaceShip.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
