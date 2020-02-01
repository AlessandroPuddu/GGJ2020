using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> puppetsPrefabs;
    public Transform puppetOrigin;
    public Transform puppetStopPoint;
    public Transform puppetDeadEnd;

    public int level;
    private int maxLevel;
    private GameObject currentPuppet;

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
        if (currentPuppet)
        {
            currentPuppet.GetComponent<PuppetMovement>().SetEndpoint(puppetDeadEnd);
        }
    }

    private void NextLevel()
    {
        level += 1;
    }
}
