using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private AudioSource source;

    public GameObject primaryHand;
    public GameObject secondaryHand;

    public GameObject projectile;
    public GameObject simulProjectile;

    public GameObject ammoLeft;
    public GameObject ammoRight;

    public float shootSpeed;
    public float increaseSpeed;
    public float maxSpeed;
    private float shootSpeedLeft;
    private float shootSpeedRight;
    public float spawnRateSimulation;
    private float lastRightSimulationTime;
    private float lastLeftSimulationTime;

    private GameObject lastSimulationLeft;
    private GameObject lastSimulationRight;

    public float minAccToCancel;

    private Transform toGoLeft;
    private Transform toGoRight;
    private bool startLeft;
    private bool startRight;

    //SECOND MODE STUFF
    public bool secondMode = false;
    public GameObject secondProjectile;
    public float laserDistance;
    public GameObject leftLaser;
    public GameObject rightLaser;
    private LineRenderer LeftLine;
    private LineRenderer RightLine;

    //menu interaction
    public bool firstScene = false;
    public bool menuInteraction = false;
    public bool endInteraction = false;
    private bool insideItemRight = false;
    private bool insideItemLeft = false;
    private ItemMenu lastImRight;
    private ItemMenu lastImLeft;
    public bool poolScene;
    public GameObject menu;
   


    // Start is called before the first frame update
    void Start()
    {
        // source = GetComponent<AudioSource>();
        //source.clip = clip;
        shootSpeedLeft = shootSpeedRight = shootSpeed;
        LeftLine = leftLaser.GetComponent<LineRenderer>();
        RightLine = rightLaser.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.A)||OVRInput.GetDown(OVRInput.Button.Start)) && !firstScene&&!endInteraction)
        {
            menuInteraction = !menuInteraction;
            if(!poolScene)LevelManager.Instance.Pause(menuInteraction);
            menu.SetActive(!menu.activeSelf);
            if (!menuInteraction)
            {
                leftLaser.SetActive(false);
                rightLaser.SetActive(false);
            }
        }
        if (menuInteraction || firstScene)
        {
            menuUpdate();
            return;
        }

        if (poolScene) return;

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            secondMode = !secondMode;
        }

        if (!secondMode)
        {
            firstModeUpdate();
        }
        else
        {
            secondModeUpdate();
        }
    }

    private void menuUpdate()
    {
        Lasers();
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (insideItemLeft)
            {
                menuAction(lastImLeft.myBuildSceneNumber);
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (insideItemRight)
            {
                menuAction(lastImRight.myBuildSceneNumber);
            }

        }

    }

    private void menuAction(int buildScene)
    {
        if (buildScene >= 0)
        {
            GameManager.Instance.loadScene(buildScene);
        }
        else if(buildScene == -1)
        {
            //retry
            GameManager.Instance.RetryLevel();
        }else if(buildScene == -2)
        {
            //next
            GameManager.Instance.NextLevel();
        }
    }

    public void firstModeUpdate()
    {
        if (OVRInput.GetLocalControllerAcceleration(OVRInput.Controller.LTouch).sqrMagnitude >= minAccToCancel)
        {
            Destroy(lastSimulationLeft);
        }
        if (OVRInput.GetLocalControllerAcceleration(OVRInput.Controller.RTouch).sqrMagnitude >= minAccToCancel)
        {
            Destroy(lastSimulationRight);
        }
        if (OVRInput.GetDown(OVRInput.Touch.PrimaryIndexTrigger))
        {
            toGoLeft = primaryHand.transform;
            startLeft = true;
        }
        if (OVRInput.GetDown(OVRInput.Touch.SecondaryIndexTrigger))
        {
            toGoRight = secondaryHand.transform;
            startRight = true;
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            Charge(true);
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            Charge(false);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            Shoot(true);
            startLeft = false;
        }
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            Shoot(false);
            startLeft = false;
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)
            && (OVRInput.GetLocalControllerAcceleration(OVRInput.Controller.RTouch).sqrMagnitude <= minAccToCancel))
        {
            simulShoot(true);
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger) &&
            OVRInput.GetLocalControllerAcceleration(OVRInput.Controller.RTouch).sqrMagnitude <= minAccToCancel)
        {
            simulShoot(false);
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            ammoLeft.SetActive(true);
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            ammoRight.SetActive(true);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            ammoLeft.SetActive(false);
        }
        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            ammoRight.SetActive(false);
        }
    }
    public void secondModeUpdate()
    {
        if (OVRInput.GetDown(OVRInput.Touch.PrimaryIndexTrigger) && !startLeft)
        {
            toGoLeft = primaryHand.transform;
            startLeft = true;
        }
        if (OVRInput.GetDown(OVRInput.Touch.SecondaryIndexTrigger) && !startLeft)
        {
            toGoRight = secondaryHand.transform;
            startRight = true;
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            Charge(true);
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            Charge(false);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            spawnAndGo(true, secondProjectile);
            GetComponent<VibrationManager>().Shoot(true);
            startLeft = false;
        }
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            spawnAndGo(false, secondProjectile);
            GetComponent<VibrationManager>().Shoot(false);
            startRight = false;
        }

        Lasers();

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
        Transform chosenHand = primary ? (startLeft ? toGoLeft:primaryHand.transform) : (startRight ? toGoRight:secondaryHand.transform);
        LineRenderer laser = primary ? LeftLine : RightLine;
        Vector3 startPosition = chosenHand.transform.position;
        Vector3 forwardDirection = chosenHand.transform.forward;
        laser.SetPosition(0, startPosition);
        laser.SetPosition(1, forwardDirection * laserDistance);

        if (menuInteraction)
        {
            RaycastHit hit;
            LayerMask layerMask = 1<<10;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(startPosition, forwardDirection, out hit, laserDistance, layerMask))
            {
                LastItemMenuAssign(primary, hit.collider.gameObject.GetComponent<ItemMenu>());

            }
            else
            {
                LastItemMenuRemove(primary);
            }
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

    private void Charge(bool primary)
    {
        if (primary)
        {
            if (shootSpeedLeft < maxSpeed)
                shootSpeedLeft += increaseSpeed * Time.deltaTime;
        }
        else
        {
            if(shootSpeedRight < maxSpeed)
            {
                shootSpeedRight += increaseSpeed * Time.deltaTime;
            }
        }
    }

    private void spawnAndGo(bool primary,GameObject toSpawn)
    {
        if (!secondMode)
        {
            //FIRST MDOE
            Transform Origin = primary ? (startLeft ? toGoLeft : primaryHand.transform) : (startRight ? toGoRight : secondaryHand.transform);
            GameObject temp = Instantiate(toSpawn, Origin.position, Quaternion.identity);
            if (temp.GetComponent<Attractor>().notAttracting)
            {
                if (primary)
                {
                    lastSimulationLeft = temp;
                }
                else
                {
                    lastSimulationRight = temp;
                }
            }
            else
            {
                temp.GetComponent<Attractor>().planetShot = true;
            }
            temp.GetComponent<Rigidbody>().AddForce(Origin.forward * (primary ? shootSpeedLeft : shootSpeedRight), ForceMode.Impulse);
        }
        else
        {
            //IF INSIDE SECOND MODE
            LineRenderer laser = primary ? LeftLine : RightLine;
            GameObject temp = Instantiate(toSpawn, laser.GetPosition(0), Quaternion.identity);
            temp.GetComponent<Rigidbody>().AddForce((laser.GetPosition(1) - laser.GetPosition(0)).normalized * (primary ? shootSpeedLeft : shootSpeedRight), ForceMode.Impulse);

        }

    }


    private void Shoot(bool primary)
    {
        if (!LevelManager.Instance.CanShot())
        {
            GetComponent<VibrationManager>().Denial(primary);
            return;
        }
        LevelManager.Instance.Shot();
        spawnAndGo(primary, projectile);
        if (primary)
        {
            lastLeftSimulationTime += 1;
            shootSpeedLeft = shootSpeed;
        }
        else
        {
            lastRightSimulationTime += 1;
            shootSpeedRight = shootSpeed;
        }
        GetComponent<VibrationManager>().Shoot(primary);
    }

    private void simulShoot(bool primary)
    {
        if (primary)
        {
            if(Time.time >= lastLeftSimulationTime + spawnRateSimulation)
            {
                spawnAndGo(primary, simulProjectile);
                lastLeftSimulationTime = Time.time;
            }
        }
        else
        {
            if (Time.time >= lastRightSimulationTime + spawnRateSimulation)
            {
                spawnAndGo(primary, simulProjectile);
                lastRightSimulationTime = Time.time;
            }
        }
        
    }
}
