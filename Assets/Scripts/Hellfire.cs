using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hellfire : MonoBehaviour
{

    private AudioSource audioSource;
    public void Awake()
    {
       audioSource=  GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ToDestroy")
        {
            Destroy(other.gameObject);
            audioSource.clip = SoundManager.Instance.gimmeBurn();
            audioSource.Play();
        }
    }
}
