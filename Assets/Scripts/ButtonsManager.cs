using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void OnTriggerEnter( Collider other )
    {
        if (other.gameObject.layer == gameObject.layer)
        {
            GetComponent<AudioSource>().Play();
            if ( gameObject.name == "DoneButton" )
                gameManager.PuppetDoneButton();
            else if ( gameObject.name == "NextButton" )
                gameManager.NextPuppetButton();
        }
    }
}
