using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using UnityEngine.UI;

public class Communication : MonoBehaviour
{
    [SerializeField] private Movement cube;
    [SerializeField] private Buzzer buzzer;
    [SerializeField] private Text textField;
    
    
    private SerialPort _port = new SerialPort("COM4", 115200);

    private float x, y, temp;
    Thread thread;
    private string receivedSignal;
    void Start()
    {
        x = 0;
        y = 0;
        temp = 0;
        thread  = new Thread(StartSerialCommunication);
        thread.Start();
    }

    private void Update()
    {
        textField.text = $"{temp} C";
    }

    void StartSerialCommunication()
    {
        _port.ReadTimeout = 1000;
        _port.WriteTimeout = 1000;
        _port.Open();
       // Debug.Log("Port Opened");
        while (_port.IsOpen)
        {
            ReadSerialData();
        }
    }

    void ReadSerialData()
    {
        try
        {
            
            receivedSignal = _port.ReadLine();

            string[] parts = receivedSignal.Split(';');

            if (parts.Length == 3)
            {
                x = float.Parse(parts[0]); 
                y = float.Parse(parts[1]);
                temp = float.Parse(parts[2]);
                Debug.Log($"X = {x}   Y = {y}   Temp = {temp}");
                cube.CallMove(x/250, y/-250);

                buzzer.alarmIsOn = temp > 21f;
            }
        }
        catch (TimeoutException e){}
    }

    public void SendSignal(LightType lightType)
    {
        try
        {
            _port.WriteLine(GetMessage(lightType));
        }
        catch (TimeoutException ignored){}
        
    }
    
    private void OnApplicationQuit()
    {
        _port.Close();
        thread.Abort();
        //Debug.Log("Port Closed");
        
    }

    string GetMessage(LightType lightType)
    {
        if (lightType == LightType.RED)
        {
            return "R";
        }
        else if (lightType == LightType.GREEN)
        {
            return "G";
        }
        else return "B";
    }
}