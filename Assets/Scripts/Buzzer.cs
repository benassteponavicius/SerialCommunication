using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buzzer : MonoBehaviour
{
    private Light light;
    public bool alarmIsOn = false;
    void Start()
    {
        light = GetComponent<Light>();
        
        InvokeRepeating("Blink", 0f, 1f);
    }

    

    void Blink()
    {
        if (alarmIsOn)
        {
            light.intensity = light.intensity == 10 ? 1 : 10;
        }
        else light.intensity = 1;
    }
}
