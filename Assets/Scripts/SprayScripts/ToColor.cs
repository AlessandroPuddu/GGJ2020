using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToColor : MonoBehaviour
{
    public enum Type { 
        dirty,
        green,
        pink,
        jew
    }
    
    [SerializeField]
    private Type type;
    
    public Type _Type { 
        get { return type;}    
    }

    private Color startColor;

    private Renderer rend;
    [SerializeField]
    private Material cleanMaterial;

    private bool isBeingHit = false;
    private bool isFullWihite = false;

    //[SerializeField]
    private float timeNeedToBecomeWhite = .5f;

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

    private void Start()
    {
        startColor = rend.material.color;
    }

    private void Update()
    {
        if (!isFullWihite) {
            if (isBeingHit)
            {
                if (timeNeedToBecomeWhite <= Time.deltaTime)
                {

                    switch (type) { 
                        case Type.dirty:
                            rend.material = cleanMaterial;
                            break;
                        case Type.green:
                            //rend.material.color = startColor;
                            rend.material = cleanMaterial;
                            break;
                        case Type.jew:
                            rend.material = cleanMaterial;
                            break;
                        case Type.pink:
                            //rend.material.color = startColor;
                            rend.material = cleanMaterial;
                            break;
                    }
                    isFullWihite = true;
                    Puppet puppet = transform.root.gameObject.GetComponent<Puppet>();
                    puppet.ChangePuppetPartColor( gameObject.name );
                }
                else
                { 
                    switch (type) { 
                        case Type.green:
                            //rend.material.color = Color.Lerp(rend.material.color, Color.white, Time.deltaTime / timeNeedToBecomeWhite);
                            break;
                        case Type.pink:
                            //rend.material.color = Color.Lerp(rend.material.color, Color.white, Time.deltaTime / timeNeedToBecomeWhite);
                            break;
                        default:
                            break;
                    }
                    

                    // update the timer
                    timeNeedToBecomeWhite -= Time.deltaTime;
                }
            }
        }

        if(isBeingHit)
            isBeingHit = false;
    }

    public void NotifyHitInThisFrame() { 
        isBeingHit = true;    
    }

    /*
    public void NotifyStopHit() { 
        isBeingHit = false;    
    }
    */
}
