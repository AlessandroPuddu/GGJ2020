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

    private int level;
    private GameObject currentPuppet;

    private void Start()
    {
        level = 0;
    }

    public void NextPuppetButton()
    {
        NextLevel();
        currentPuppet = Instantiate(puppetsPrefabs[0], puppetOrigin.position, puppetOrigin.rotation);
        currentPuppet.GetComponent<PuppetMovement>().SetEndpoint(puppetStopPoint);
    }

    public void PuppetDoneButton()
    {
        currentPuppet.GetComponent<PuppetMovement>().SetEndpoint(puppetDeadEnd);
    }

    private void NextLevel()
    {
        level += 1;
    }
}
