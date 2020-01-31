using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    private bool _rotate;
    public bool X = false;
    public bool Y = false;
    public bool Z = false;


    public int myBuildSceneNumber;

    public void Rotate(bool inside)
    {
        _rotate = inside;
    }

    public void Update()
    {
        float rotateX = !X ? 0 : Time.deltaTime * 5;
        float rotateY = !Y ? 0 : Time.deltaTime * 5;
        float rotateZ = !Z ? 0 : Time.deltaTime * 5;
        if (!_rotate)
        {
            transform.Rotate(rotateX, rotateY, rotateZ, Space.Self);
        }
        else
        {
            transform.Rotate(rotateX*5, rotateY*5, rotateZ*5, Space.Self);
        }
    }



}
