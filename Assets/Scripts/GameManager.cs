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

    public int level;
    private int maxLevel;
    private GameObject currentPuppet;

    public TextMeshProUGUI timetext;
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
        level = 0;
        maxLevel = puppetsPrefabs.Count;
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
                Debug.Log("GameOver");
            }
        }
    }

    public void PuppetDoneButton()
    {
        if (currentPuppet && currentPuppet.GetComponent<PuppetMovement>().IsPuppetStop())
        {
            currentPuppet.GetComponent<PuppetMovement>().SetEndpoint(puppetDeadEnd);
        }
    }

    private void NextLevel()
    {
        level += 1;
        if (level > 1)
        {
            StopCoroutine(timer);
        }
        timer = StartCoroutine("CheckTimer");
    }

    private void GameOver()
    {
        timetext.text = "Lost";
    }

    IEnumerator CheckTimer()
    {
        int currentTime = tempoMedio;
        while(currentTime > 0)
        {
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
    }

    private void LevelComplete()
    {
        Debug.Log("Level " + (level - 1) + " complete!!!");
    }
}
