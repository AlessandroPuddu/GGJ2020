using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public List<GameObject> puppetsPrefabs;
    public Transform puppetOrigin;
    public Transform puppetStopPoint;
    public Transform puppetDeadEnd;

    [System.Serializable] public struct Effect
    {
        public string text;
        public AudioClip voiceOver;
    }

    public List<Effect> screenContentBetweenLevels = new List<Effect>();

    public int level;


    private int maxLevel;
    private bool conveyorBeltMovement;

    private GameObject currentPuppet;

    public TextMeshProUGUI timetext;
    public GameObject targetPanel;
    public int tempoMedio;
    Coroutine timer;

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
    }

    private void Start()
    {
        conveyorBeltMovement = false;
        level = 0;
        maxLevel = puppetsPrefabs.Count;
        timetext.text = screenContentBetweenLevels[0].text;
    }

    public void NextPuppetButton()
    {
        if (!currentPuppet)
        {
            if (level < maxLevel)
            {
                currentPuppet = Instantiate(puppetsPrefabs[level], puppetOrigin.position, puppetOrigin.rotation);
                currentPuppet.GetComponent<PuppetMovement>().SetEndpoint(puppetStopPoint);
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
        int currentTime = tempoMedio;
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
        /*
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
        StopCoroutine(timer);
        Debug.Log("Level " + (level - 1) + " complete!!!");
        timetext.alignment = TextAlignmentOptions.Midline;
        targetPanel.SetActive(false);
        timetext.text = screenContentBetweenLevels[level].text;
        //reproduce audio voice over
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
