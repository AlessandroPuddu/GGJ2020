using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> puppetsPrefabs;
    [SerializeField] Transform puppetOrigin;
    [SerializeField] Transform puppetStopPoint;

    private int level;
    private GameObject currentPuppet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextPuppetButton()
    {
        NextLevel();
        currentPuppet = Instantiate(puppetsPrefabs[level], puppetOrigin.position, puppetOrigin.rotation);
    }

    private void PuppetDoneButton()
    {

    }

    private void NextLevel()
    {
        level += 1;
    }
}
