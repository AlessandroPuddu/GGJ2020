using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> myList = new List<GameObject>();
    public int maxCharges;
    private int actualCharge;
    public Text textLeft;
    public Text textRight;

    public GameObject endMenu;
    public GameObject loseMenu;
    public Shooting shoot;
    [Tooltip("Time after the launch of the last available planet")]
    public float timeBeforeGameOver;

    [Tooltip("Seconds you have to win the level. 0 if infinite.")]
    public float durationOfTheLevel;
    private float timeCounting;
    public Text timeText;
    private float startTime;

    public GameObject poem;

    private bool pause = false;

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            textLeft.text = "" + maxCharges;
            textRight.text = "" + maxCharges;
            actualCharge = maxCharges;

            startTime = Time.time;
            if(durationOfTheLevel == 0)
            {
                timeText.enabled = false;
                this.enabled = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (pause) return;
        timeCounting = Time.time - startTime;
        timeCounting = durationOfTheLevel - timeCounting; 
        if (timeCounting <= 0)
        {
            GameOver();
            textLeft.text = "0";
            textRight.text = "0";
            timeText.text = "0";
            this.enabled = false;
        }
        else
        {
            timeText.text = "" + (int)timeCounting;
        }
    }

    public bool CanShot()
    {
        return actualCharge > 0;
    }

    public void Pause(bool toPause)
    {
        if (toPause)
        {
            pause = true;
        }
        else
        {
            pause = false;
            startTime = Time.time -(durationOfTheLevel-timeCounting);
        }
    }

    public void Shot()
    {
        actualCharge--;
        textLeft.text = "" + actualCharge;
        textRight.text = "" + actualCharge;
        if (actualCharge == 0)
        {
            Invoke("GameOver", timeBeforeGameOver);
        }
    }

    public void GameOver()
    {
        if (!endMenu.activeSelf)
        {
            loseMenu.SetActive(true);
            shoot.endInteraction = true;
            shoot.menuInteraction = true;
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
            shoot.menuInteraction = true;
            shoot.endInteraction = true;
            Invoke("Poem", 0.2f);

        }
    }
    private void Poem()
    {
        poem.SetActive(true);
        this.transform.parent.gameObject.SetActive(false);
    }
}
