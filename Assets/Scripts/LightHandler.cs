using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightType
{
    RED,
    GREEN,
    BLUE
};
public class LightHandler : MonoBehaviour
{

    public LightType Type;
    [SerializeField] private Communication com;
    [SerializeField] private Light light;
    private bool isOn = false;

    private void OnMouseUp()
    {
        Debug.Log(Type);
        com.SendSignal(Type);

        if (!isOn)
        {
            light.intensity = 1f;
            isOn = true;
        }
        else
        {
            light.intensity = 0f;
            isOn = false;
        }
    }
}
