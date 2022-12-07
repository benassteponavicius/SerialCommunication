using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;

public class SendToDB : MonoBehaviour
{
    private string databaseName = "/DATABASE/antiFloodSystem.db";
    private SqliteConnection databaseConnection;
    private SqliteCommand databaseCommand;
    
    [SerializeField] private CommunicationCyber communicationCyber;
    
    void Start()
    {
        string filepath = "URI=file:" + Application.dataPath + databaseName;
        databaseConnection = new SqliteConnection(filepath);
        
        StartCoroutine(sendDataToDB());
    }


    private SensorData getSensorData()
    {
        SensorData sensorData = new SensorData(0, (int)communicationCyber.humidity, (int)communicationCyber.water, DateTime.Now);
        return sensorData;
    }

    private void SendData()
    {
        using (databaseConnection)
        {
            SensorData sensorData = getSensorData();
            databaseConnection.Open();
            databaseCommand = databaseConnection.CreateCommand();
            
            string sqlQuery = "INSERT INTO SENSORDATA (humidity,waterlevel,date) VALUES ('" + sensorData.getHumidity() + "','" + sensorData.getWaterLevel() + "','" + sensorData.getDate() + "');";

            databaseCommand.CommandText = sqlQuery;
            databaseCommand.ExecuteScalar();
            databaseConnection.Close();
        }
    }

    public List<SensorData> GetDataFromDB()
    {
        using (databaseConnection)
        {
            List<string> list = new List<string>();
            List<SensorData> dataList = new List<SensorData>();
            databaseConnection.Open();
            databaseCommand = databaseConnection.CreateCommand();
            SqliteDataReader reader;

            string sqlQuery = "SELECT * FROM SENSORDATA";
                
            databaseCommand.CommandText = sqlQuery;
            reader = databaseCommand.ExecuteReader();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    list.Add(reader.GetValue(i).ToString());
                }
                
            }

            for (int i = 0; i < list.Count-4; i+=4)
            {
                dataList.Add(new SensorData(int.Parse(list[i]), int.Parse(list[i+1]),int.Parse(list[i+2]), DateTime.Parse(list[i+3])));
            }

            databaseConnection.Close();

            return dataList;

        }
    }

    IEnumerator sendDataToDB()
    {
        SendData();
        yield return new WaitForSeconds(20);
        StartCoroutine(sendDataToDB());
    }
}
