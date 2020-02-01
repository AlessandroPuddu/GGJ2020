using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public List<GameObject> puppetsPrefabs;
    public Transform puppetOrigin;
    public Transform puppetStopPoint;
    public Transform puppetDeadEnd;

    public int level;
    private int maxLevel;
    private bool conveyorBeltMovement;
    private GameObject currentPuppet;

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
        if (currentPuppet && !conveyorBeltMovement)
        {
            currentPuppet.GetComponent<PuppetMovement>().SetEndpoint(puppetDeadEnd);
        }
    }

    private void NextLevel()
    {
        level += 1;
    }

    private void GameOver()
    {

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

    public void SetConveyorBeltMovement(bool isMoving)
    {
        conveyorBeltMovement = isMoving;
    }

    public bool IsConveyorBeltMoving()
    {
        return conveyorBeltMovement;
    }
}
