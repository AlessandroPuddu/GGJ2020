using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private PuppetsManager puppetsManger;
    public Transform puppetOrigin;
    public Transform puppetStopPoint;
    public Transform puppetDeadEnd;

    private int level;
    private int maxLevel;
    private bool conveyorBeltMovement;

    private GameObject currentPuppet;
    private Coroutine timer;

    [System.Serializable] public struct Effect
    {
        public string text;
        public AudioClip voiceOver;
    }

    public List<Effect> screenContentBetweenLevels = new List<Effect>();
    public TextMeshProUGUI timetext;
    public GameObject targetPanel;
    public int roundTimer;
    public AudioSource monitorAudio;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        puppetsManger = GameObject.FindGameObjectWithTag("PuppetsManager").GetComponent<PuppetsManager>();

    }

    private void Start()
    {
        conveyorBeltMovement = false;
        level = 0;
        maxLevel = puppetsManger.puppets.Length;
        timetext.text = screenContentBetweenLevels[0].text;
    }

    public void NextPuppetButton()
    {
        if (!currentPuppet)
        {
            if (level < maxLevel)
            {
                currentPuppet = Instantiate(puppetsManger.puppets[level], puppetOrigin.position, puppetOrigin.rotation);
                currentPuppet.GetComponent<PuppetMovement>().SetEndpoint(puppetStopPoint);
                puppetsManger.SetCurrentPuppet(currentPuppet);
                NextLevel();
                
            } else
            {
                targetPanel.SetActive(false);
                timetext.text = "No one is different.";
            }
        }
    }

    public void PuppetDoneButton()
    {
        if (currentPuppet && !conveyorBeltMovement)
        {
            currentPuppet.GetComponent<PuppetMovement>().SetEndpoint(puppetDeadEnd);
        }
    }

    private void NextLevel()
    {
        level += 1;
        targetPanel.SetActive(true);
        timer = StartCoroutine("CheckTimer");

    }

    private void GameOver()
    {
        timetext.alignment = TextAlignmentOptions.Midline;
        targetPanel.SetActive(false);
        timetext.text = "Lost";
    }

    IEnumerator CheckTimer()
    {
        int currentTime = roundTimer;
        while(currentTime > 0)
        {
            timetext.alignment = TextAlignmentOptions.MidlineLeft;
            timetext.text = ""+currentTime--;
            yield return new WaitForSeconds(1f);
        }
        GameOver();
    }

    public void PuppetCheck(GameObject puppet)
    {
        /*TODO
        inserisci qui i controlli per il puppet e ritornami true o false da mettere nell'if
        */
        if (true)
        {
            LevelComplete();
        } else
        {
            RepeateLevel();
        }
    }

    private void RepeateLevel()
    {
        level -= 1;
        currentPuppet = null;
        NextPuppetButton();
        StopCoroutine(timer);
    
    }

    private void LevelComplete()
    {
        if (timer != null)
        {
            StopCoroutine(timer);
        }
        
        Debug.Log("Level " + (level - 1) + " complete!!!");
        timetext.alignment = TextAlignmentOptions.Midline;
        targetPanel.SetActive(false);
        timetext.text = screenContentBetweenLevels[level].text;

        monitorAudio.clip = screenContentBetweenLevels[level].voiceOver;
        monitorAudio.Play();
    }

    public void SetConveyorBeltMovement(bool isMoving)
    {
        conveyorBeltMovement = isMoving;
    }

    public bool IsConveyorBeltMoving()
    {
        return conveyorBeltMovement;
    }
}
