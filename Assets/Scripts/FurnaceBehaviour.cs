using UnityEngine;

public class FurnaceBehaviour : MonoBehaviour
{
    public GameObject furnaceTop;
    public Transform grateStartingPosition;
    public Transform grateEndingPosition;

    private bool move;
    private bool open;

    private void Start()
    {
        move = false;
        open = false;
    }

    private void Update()
    {
        if (move)
        {
            if (open)
            {
                furnaceTop.transform.position = Vector3.Lerp(furnaceTop.transform.position, grateEndingPosition.position, 0.9f * Time.deltaTime * 8);
                if (Vector3.Distance(grateEndingPosition.position, furnaceTop.transform.position) < 0.01f)
                {
                    move = false;
                }
            } else
            {
                furnaceTop.transform.position = Vector3.Lerp(furnaceTop.transform.position, grateStartingPosition.position, 0.9f * Time.deltaTime * 8);
                if (Vector3.Distance(grateStartingPosition.position, furnaceTop.transform.position) < 0.01f)
                {
                    move = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            move = true;
            open = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            move = true;
            open = false;
        }
    }

}
