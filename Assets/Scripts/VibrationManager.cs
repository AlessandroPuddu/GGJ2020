using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    private float leftRequestTime;
    private float rightRequestTime;

    private float leftStartVibrate;
    private float rightStartVibrate;

    public vibrationConfig[] configs;

    private bool leftCharging;
    private bool rightCharging;

    [System.Serializable]
    public struct vibrationConfig
    {
        public string type;
        public float time;
        public float frequency;
        public float amplitude;
    }

    public static VibrationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (leftRequestTime < Time.time- leftStartVibrate)
        {
            stopVibration(true);
        }
        if(rightRequestTime< Time.time - rightStartVibrate)
        {
            stopVibration(false);
        }
    }
    public void Denial(bool primary)
    {
        vibrationConfig shoot = configs[1];
        Vibrate(shoot.time, shoot.frequency, shoot.amplitude, primary);
    }

    public void Inside(bool primary)
    {
        vibrationConfig shoot = configs[2];
        Vibrate(shoot.time, shoot.frequency, shoot.amplitude, primary);
    }

    public void Shoot(bool primary)
    {
        vibrationConfig shoot = configs[0];
        Vibrate(shoot.time, shoot.frequency, shoot.amplitude, primary);
    }

    public void Vibrate(float time, float frequency, float amplitude, bool primary)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, primary ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch);
        if (primary) {
            leftStartVibrate = Time.time;
            leftRequestTime = time;
        }
        else
        {
            rightStartVibrate = Time.time;
            rightRequestTime = time;
        }
    }

    public void stopVibration(bool primary)
    {
        OVRInput.SetControllerVibration(0, 0, primary ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch);
    }
}

