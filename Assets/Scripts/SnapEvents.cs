using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapEvents : MonoBehaviour
{
    public delegate void OnSnapNeed();
    public event OnSnapNeed onSnapNeed;

    public void RaiseOnSnapNeed() { 
        if(onSnapNeed != null) { 
            onSnapNeed();    
        }   
    }
}
