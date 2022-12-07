using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using UnityEngine.UI;

public class CommunicationCyber : MonoBehaviour
{
    
    [SerializeField] private Text tempField;
    [SerializeField] private Text humidityField;
    [SerializeField] private Text waterField;
    [SerializeField] private Slider waterSlider;

    [SerializeField] private InputField tempLimitField;
    [SerializeField] private InputField waterLimitField;
    
    
    private SerialPort _port = new SerialPort("COM4", 9600);

    public float humidity, water, temp, tempLimit, waterLimit;
    Thread thread;
    private string receivedSignal;
    void Start()
    {
        _port = new SerialPort(PlayerPrefs.GetString("Port"), PlayerPrefs.GetInt("BaudRate"));
        
        humidity = 0;
        water = 0;
        temp = 0;
        tempLimit = 22;
        waterLimit = 100;
        thread  = new Thread(StartSerialCommunication);
        thread.Start();
    }

    private void Update()
    {
        tempField.text = $"Temperature: {temp} C";
        humidityField.text = $"Humidity: {humidity} %";
        waterField.text = $"Water level: {water}";
        waterSlider.value = water;

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
                humidity = float.Parse(parts[0]); 
                temp = float.Parse(parts[1]);
                water = float.Parse(parts[2]);
            }
        }
        catch (TimeoutException e){}
    }

    public void SendSignal()
    {
        tempLimit = float.Parse(tempLimitField.text);
        waterLimit = float.Parse(waterLimitField.text);
        try
        {
            _port.WriteLine($"{tempLimit}:{waterLimit};");
            Debug.Log($"{tempLimit}:{waterLimit};");
        }
        catch (TimeoutException ignored){}
        
    }
    
    private void OnApplicationQuit()
    {
        _port.Close();
        thread.Abort();
        //Debug.Log("Port Closed");
        
    }

}