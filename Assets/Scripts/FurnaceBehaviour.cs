using UnityEngine;

public class FurnaceBehaviour : MonoBehaviour
{
    public GameObject furnaceTop;
    public Transform grateStartingPosition;
    public Transform grateEndingPosition;

    private bool move;
    private bool open;

    public AudioSource grigliaSource;
    public AudioSource ovenFire;

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
            if (grigliaSource.isPlaying)
            {
                grigliaSource.Stop();
            }
            grigliaSource.Play();
            open = true;
            grigliaSource.volume *= 4;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            if (grigliaSource.isPlaying)
            {
                grigliaSource.Stop();
            }
            grigliaSource.Play();
            move = true;
            open = false;
            grigliaSource.volume /= 4;
        }
    }

}
