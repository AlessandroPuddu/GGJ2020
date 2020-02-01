using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public delegate void OnRightPartPlaced();

    public static event OnRightPartPlaced onRightPartPlaced;

    public static void RaiseOnRightPartPlaced() { 
        if(onRightPartPlaced != null) { 
            onRightPartPlaced();    
        }
    }
}
