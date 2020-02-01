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

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter( Collider other )
    {
        if (other.gameObject.layer == gameObject.layer)
        {
            gameManager.NextPuppetButton();
        }
    }
}
