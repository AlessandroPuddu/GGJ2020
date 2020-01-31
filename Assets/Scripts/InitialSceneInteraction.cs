using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSceneInteraction : MonoBehaviour
{
    public GameObject primaryHand;
    public GameObject secondaryHand;
    private bool startLeft;
    private bool startRight;
    private Transform toGoLeft;
    private Transform toGoRight;

    public float laserDistance;
    public GameObject leftLaser;
    public GameObject rightLaser;
    private LineRenderer LeftLine;
    private LineRenderer RightLine;

    //menu interaction
    private bool insideItemRight = false;
    private bool insideItemLeft = false;
    private ItemMenu lastImRight;
    private ItemMenu lastImLeft;

    // Start is called before the first frame update
    void Start()
    {
        LeftLine = leftLaser.GetComponent<LineRenderer>();
        RightLine = rightLaser.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Lasers();
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (insideItemLeft)
            {
                GameManager.Instance.loadScene(lastImLeft.myBuildSceneNumber);
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (insideItemRight)
            {
                GameManager.Instance.loadScene(lastImRight.myBuildSceneNumber);
            }

        }
    }

    private void Lasers()
    {
        //LASER HANDLING
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            leftLaser.SetActive(true);
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            rightLaser.SetActive(true);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            Ray(true);
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            Ray(false);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            LastItemMenuRemove(true);
            leftLaser.SetActive(false);
        }
        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            LastItemMenuRemove(false);
            rightLaser.SetActive(false);
        }
    }
    private void Ray(bool primary)
    {
        Transform chosenHand = primary ? (startLeft ? toGoLeft : primaryHand.transform) : (startRight ? toGoRight : secondaryHand.transform);
        LineRenderer laser = primary ? LeftLine : RightLine;
        Vector3 startPosition = chosenHand.transform.position;
        Vector3 forwardDirection = chosenHand.transform.forward;
        laser.SetPosition(0, startPosition);
        laser.SetPosition(1, forwardDirection * laserDistance);
        RaycastHit hit;
        LayerMask layerMask = 1 << 10;
      
        if (Physics.Raycast(startPosition, forwardDirection, out hit, laserDistance, layerMask))
        {
            LastItemMenuAssign(primary, hit.collider.gameObject.GetComponent<ItemMenu>());

        }
        else
        {
            LastItemMenuRemove(primary);
        }
        
    }

    private void LastItemMenuAssign(bool primary, ItemMenu im)
    {
        if (primary)
        {
            lastImLeft = im;
            insideItemLeft = true;
            lastImLeft.Rotate(true);
        }
        else
        {
            lastImRight = im;
            insideItemRight = true;
            lastImRight.Rotate(true);
        }

    }

    private void LastItemMenuRemove(bool primary)
    {
        if (primary)
        {
            if (insideItemLeft)
            {
                lastImLeft.Rotate(false);
                insideItemLeft = false;
            }
        }
        else
        {
            if (insideItemRight)
            {
                lastImRight.Rotate(false);
                insideItemRight = false;
            }
        }

    }
}
