using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PrintDB : MonoBehaviour
{
    [SerializeField]
    private SendToDB database;

    [SerializeField] private Text textPrefab;
    [SerializeField] private GameObject panel;
    [SerializeField] private Text dataText;
    
    private List<SensorData> list;



    void Start()
    {
        StartCoroutine(getDataFromDB());
    }
    
    
    IEnumerator getDataFromDB()
    {
        list = database.GetDataFromDB();
        PrintData();
        yield return new WaitForSeconds(10);
        StartCoroutine(getDataFromDB());
    }

    void PrintData()
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag("sensorData");
        foreach (var obj in array)
        {
            Destroy(obj);
        }
        foreach (var value in list)
        {
            var textField = Instantiate(textPrefab, panel.transform);
            textField.text = value.ToString();
        }
        AverageStats();
    }

    private void AverageStats()
    {
        int humidity = 0;
        int waterLevel = 0;
        foreach (var value in list)
        {
            humidity += value.getHumidity();
            waterLevel += value.getWaterLevel();
        }

        dataText.text = $"Average Humidity: {humidity / list.Count}%  Average Water Level: {waterLevel / list.Count}";

    }
    
    
}
