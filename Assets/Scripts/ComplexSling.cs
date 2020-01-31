using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexSling : MonoBehaviour
{
    public GameObject primaryHand;
    public GameObject secondaryHand;

    public GameObject leftLaser;
    public GameObject rightLaser;

    public float forwardPower = 10;
    public float strength=100;

    private LineRenderer LeftLine;
    private LineRenderer RightLine;

    public GameObject fakePlanet;

    private GameObject selectedPlanet;

    private Shooting shooting;

    // Start is called before the first frame update
    void Start()
    {
        LeftLine = leftLaser.GetComponent<LineRenderer>();
        RightLine = rightLaser.GetComponent<LineRenderer>();
        shooting = this.GetComponent<Shooting>();
    }


    bool selectedLeft = false;
    // Update is called once per frame
    void Update()
    {
        if (shooting.menuInteraction) return;
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            leftLaser.SetActive(true);
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            rightLaser.SetActive(true);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            leftLaser.SetActive(false);
        }
        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            rightLaser.SetActive(false);
        }
        if (selectedPlanet == null)
        {
            FirstState();
        }
        else
        {
            secondState();
        }


        //se selezionato
        //aggiorna la freccia da una mano all'altra
        //e la freccia che sbuca dalla 
    }

    private void secondState()
    {
        if (selectedLeft)
        {
            fakePlanet.transform.position = primaryHand.transform.position;
            RightLine.SetPosition(0, secondaryHand.transform.position);
            RightLine.SetPosition(1, primaryHand.transform.position);
            LeftLine.SetPosition(0, selectedPlanet.transform.position);

            Vector3 direction = (primaryHand.transform.position - secondaryHand.transform.position);
            LeftLine.SetPosition(1, selectedPlanet.transform.position + direction*2);

           if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger)|| OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger)){

                selectedPlanet.GetComponent<Rigidbody>().AddForce(direction * strength);
                VibrationManager.Instance.Shoot(false);
                selectedPlanet = null;
                fakePlanet.SetActive(false);
                rightLaser.SetActive(false);
                leftLaser.SetActive(false);
            }
            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
            {
                selectedPlanet = null;
                fakePlanet.SetActive(false);
                rightLaser.SetActive(false);
                leftLaser.SetActive(false);
            }

        }
        else
        {
            fakePlanet.transform.position = secondaryHand.transform.position;

            LeftLine.SetPosition(0, primaryHand.transform.position);
            LeftLine.SetPosition(1, secondaryHand.transform.position);
            RightLine.SetPosition(0, selectedPlanet.transform.position);

            Vector3 direction = (secondaryHand.transform.position - primaryHand.transform.position);
            RightLine.SetPosition(1, selectedPlanet.transform.position + direction*2);

            if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
            {
                selectedPlanet.GetComponent<Rigidbody>().AddForce(direction * strength);
                selectedPlanet = null;
                VibrationManager.Instance.Shoot(true);
                fakePlanet.SetActive(false);
                rightLaser.SetActive(false);
                leftLaser.SetActive(false);
            }
            if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger)) 
            {
                selectedPlanet = null;
                fakePlanet.SetActive(false);
                rightLaser.SetActive(false);
                leftLaser.SetActive(false);
            }
        }
    }

    public void FirstState()
    {
        //update pointers
        LeftLine.SetPosition(0, primaryHand.transform.position);
        LeftLine.SetPosition(1, primaryHand.transform.position + primaryHand.transform.forward * forwardPower);
        RightLine.SetPosition(0, secondaryHand.transform.position);
        RightLine.SetPosition(1, secondaryHand.transform.position + secondaryHand.transform.forward * forwardPower);

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Vector3 direction = RightLine.GetPosition(1) - RightLine.GetPosition(0);
            RaycastHit hit;
            if (Physics.Raycast(secondaryHand.transform.position, direction.normalized*forwardPower, out hit))
            {
                if (hit.collider.gameObject.layer == 9)
                {
                    selectedPlanet = hit.collider.gameObject;
                    selectedLeft = false;
                    fakePlanet.SetActive(true);
                    rightLaser.SetActive(true);
                    leftLaser.SetActive(true);
                }
            }

        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Vector3 direction = LeftLine.GetPosition(1) - LeftLine.GetPosition(0);
            RaycastHit hit;
            if (Physics.Raycast(primaryHand.transform.position, direction.normalized*forwardPower, out hit))
            {
                if (hit.collider.gameObject.layer == 9)
                {
                    selectedPlanet = hit.collider.gameObject;
                    selectedLeft = true;
                    fakePlanet.SetActive(true);
                    leftLaser.SetActive(true);
                    rightLaser.SetActive(true);
                }
            }
        }
    }
}
