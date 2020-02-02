using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance { get { return _instance; } }

    public List<AudioSource> cilindri;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }


    }

    public void PlayRullo(bool play)
    {
        if (!play)
        {
            foreach (AudioSource a in cilindri)
            {
                a.Stop();
            }
        }
        else
        {
            foreach (AudioSource a in cilindri)
            {
                if (a.isPlaying) return;
                a.Play();
            }
        }
    }
}
