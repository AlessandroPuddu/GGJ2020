using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToColor : MonoBehaviour
{
    private Renderer rend;

    private bool isBeingHit = false;
    private bool isFullWihite = false;

    [SerializeField]
    private float timeNeedToBecomeWhite = 2.0f;

    public bool IsFullWhite { 
        get { return isFullWihite; }    
    }

    public bool IsBeingHit { 
        get { return isBeingHit; }    
    }

    private void Awake()
    {
        rend = this.gameObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        if (!isFullWihite) {
            if (isBeingHit)
            {
                if (timeNeedToBecomeWhite <= Time.deltaTime)
                {
                    isFullWihite = true;
                }
                else
                {
                    rend.material.color = Color.Lerp(rend.material.color, Color.white, Time.deltaTime / timeNeedToBecomeWhite);

                    // update the timer
                    timeNeedToBecomeWhite -= Time.deltaTime;
                }
            }
        }
    }

    public void NotifyHit() { 
        isBeingHit = true;    
    }

    public void NotifyStopHit() { 
        isBeingHit = false;    
    }
}
