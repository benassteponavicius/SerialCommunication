using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class SignalHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;

    private SerialPort _port = new SerialPort("COM4", 9600);
    
    Thread thread;
    private string receivedSignal;
    void Start()
    {
        thread  = new Thread(StartSerialCommunication);
        thread.Start();
    }

    private void Update()
    {
        _textField.text = receivedSignal;
    }

    void StartSerialCommunication()
    {
        _port.ReadTimeout = 1000;
        _port.WriteTimeout = 1000;
        _port.Open();
        Debug.Log("Port Opened");
        while (_port.IsOpen)
        {
            ReadSerialData();
        }
    }

    void ReadSerialData()
    {
        try
        {
            
            Debug.Log("Reading signal...");
            receivedSignal = _port.ReadLine();
           // _port.DiscardInBuffer();
           // _port.DiscardOutBuffer();
        }
        catch (TimeoutException e){}
    }

    public void SendSignal()
    {
        try
        {
            _port.WriteLine("A");
        }
        catch (TimeoutException ignored){}
        
    }
    
    public void SendOtherSignal()
    {
        try
        {
            _port.WriteLine("Z");
        }
        catch (TimeoutException ignored){}
        
    }
    
    private void OnApplicationQuit()
    {
        _port.Close();
        thread.Abort();
        Debug.Log("Port Closed");
        
    }
}
