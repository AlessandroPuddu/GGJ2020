using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TripLevelManager : MonoBehaviour
{


    [SerializeField]
    List<GameObject> myList = new List<GameObject>();

    public GameObject endMenu;
    public GameObject loseMenu;
    public GameObject mainMenu;

    public SpaceTripControls sp;

    private bool started = false;

    public MenuHandling mh;

    private bool pause = false;

    public static TripLevelManager Instance { get; private set; }

    [Tooltip("Seconds you have to win the level. 0 if infinite.")]
    public float durationOfTheLevel;
    private float timeCounting;
    public Text timeText;
    private float startTime;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            startTime = Time.time;
            if (durationOfTheLevel == 0)
            {
                timeText.enabled = false;
                this.enabled = false;
            }
            else
            {
                timeText.text = ""+durationOfTheLevel;
            }
        }
    }

    public void Update()
    {
        if (!started) return;
        if (Input.GetKeyDown(KeyCode.A) ||OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (pause)
            {
                if(!mh.endInteraction)Pause(false);
            }
            else
            {
                Pause(true);
            }

        }
        if (pause) return;

        timeCounting = Time.time - startTime;
        timeCounting = durationOfTheLevel - timeCounting;

        if (timeCounting <= 0)
        {
            GameOver();
            timeText.text = "0";
            this.enabled = false;
        }
        else
        {
            timeText.text = "" + (int)timeCounting;
        }
    }

    public void setUpAndStart()
    {
        startTime = Time.time;
        started = true;
    }

    public void GameOver()
    {
        if (!endMenu.activeSelf)
        {
            loseMenu.SetActive(true);
            mh.endInteraction = true;
            Pause(true);
        }
    }
    public void HitTrigger(GameObject go)
    {
        //called by targets
        myList.Remove(go);
        if (myList.Count == 0)
        {

            loseMenu.SetActive(false);
            timeText.enabled = false;
            endMenu.SetActive(true);
            mh.endInteraction = true;
            Pause(true);

        }
    }

    public void Pause(bool toPause)
    {
        pause = toPause;
        mh.menuInteraction = toPause;
        sp.Pause(toPause);
        if (!mh.endInteraction) mainMenu.SetActive(toPause);
        if (!toPause)
        {
            startTime = Time.time - (durationOfTheLevel - timeCounting);
        }
    }
}
