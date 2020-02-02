using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private PuppetsManager puppetsManger;
    public Transform puppetOrigin;
    public Transform puppetStopPoint;
    public Transform puppetDeadEnd;

    private int level;
    private int maxLevel;
    private bool conveyorBeltMovement;

    private GameObject currentPuppet;
    private Coroutine timer;

    [System.Serializable] public struct Effect
    {
        public string text;
        public AudioClip voiceOver;
    }

    public List<Effect> screenContentBetweenLevels = new List<Effect>();
    public TextMeshProUGUI timetext;
    public GameObject targetPanel;
    public float roundTimer= 15.01f;
    public AudioSource monitorAudio;

    public static GameManager Instance { get { return _instance; } }

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

        puppetsManger = GameObject.FindGameObjectWithTag("PuppetsManager").GetComponent<PuppetsManager>();
    }

    private void Start()
    {
        conveyorBeltMovement = false;
        level = 0;
        maxLevel = puppetsManger.puppets.Length;
        timetext.text = screenContentBetweenLevels[0].text;
        monitorAudio.clip = screenContentBetweenLevels[0].voiceOver;
        monitorAudio.Play();
    }

    public void NextPuppetButton()
    {
        if (!currentPuppet)
        {
            if (level < maxLevel)
            {
                currentPuppet = Instantiate(puppetsManger.puppets[level], puppetOrigin.position, puppetsManger.puppets[ level ].transform.rotation );
                currentPuppet.GetComponent<PuppetMovement>().SetEndpoint(puppetStopPoint);
                puppetsManger.SetCurrentPuppet(currentPuppet);
                NextLevel();
                
            } else
            {
                targetPanel.SetActive(false);
                timetext.text = "No one is different.";
            }
        }
    }

    public void PuppetDoneButton()
    {
        if (currentPuppet && !conveyorBeltMovement)
        {
            currentPuppet.GetComponent<PuppetMovement>().SetEndpoint(puppetDeadEnd);
        }
    }

    private void NextLevel()
    {
        level += 1;
        targetPanel.SetActive(true);
        timer = StartCoroutine("CheckTimer");
    }

    private void GameOver()
    {
        timetext.alignment = TextAlignmentOptions.Midline;
        targetPanel.SetActive(false);
        timetext.text = "Wrong";
        monitorAudio.clip = SoundManager.Instance.defeat;
        monitorAudio.Play();
    }


    IEnumerator CheckTimer()
    {
        SoundManager sm = SoundManager.Instance;
        float timeStart = Time.time;
        float timeRemaining = roundTimer;
        float timeCounting = 1;
        while(timeCounting > 0)
        {
            timeCounting = Time.time - timeStart;
            timeCounting = timeRemaining - timeCounting;
            sm.secondaryAudioFromScreen.clip = sm.getTimeBeat(timeCounting<3.75);
            sm.secondaryAudioFromScreen.Play();
            timetext.alignment = TextAlignmentOptions.MidlineLeft;
            timetext.text = ""+(int)timeCounting;

             if (timeCounting < 1)
            {
                yield return new WaitForSeconds(0.2f);
            }else if(timeCounting < 2.5)
            {
                yield return new WaitForSeconds(0.35f);
            }
            else if (timeCounting < 7.5)
            {
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }       

        }
        GameOver();
    }

    public void PuppetCheck(GameObject puppet)
    {
        if (puppet.GetComponent<Puppet>().CheckAllRight())
        {
            LevelComplete();
        } else
        {
            RepeateLevel();
        }
    }

    private void RepeateLevel()
    {
        level -= 1;
        currentPuppet = null;
        //NextPuppetButton();
        StopCoroutine(timer);
    }

    private void LevelComplete()
    {
        if (timer != null)
        {
            StopCoroutine(timer);
        }
        
        Debug.Log("Level " + (level - 1) + " complete!!!");
        timetext.alignment = TextAlignmentOptions.Midline;
        targetPanel.SetActive(false);
        timetext.text = screenContentBetweenLevels[level].text;

        monitorAudio.clip = SoundManager.Instance.victory;
        monitorAudio.Play();

        monitorAudio.clip = screenContentBetweenLevels[level].voiceOver;
        monitorAudio.Play();
    }

    public void SetConveyorBeltMovement(bool isMoving)
    {
        conveyorBeltMovement = isMoving;
        SoundManager.Instance.PlayRullo(isMoving);
    }

    public bool IsConveyorBeltMoving()
    {
        return conveyorBeltMovement;
    }
}
