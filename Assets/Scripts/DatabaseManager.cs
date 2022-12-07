using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using TMPro;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private string databaseName = "/DATABASE/antiFloodSystem.db";
    private SqliteConnection databaseConnection;
    private SqliteCommand databaseCommand;
    
    
    
    void Start()
    {
        string filepath = "URI=file:" + Application.dataPath + databaseName;
        databaseConnection = new SqliteConnection(filepath);
        
    }



    public void SendData(string username, string password)
    {
        using (databaseConnection)
        {
            databaseConnection.Open();
            databaseCommand = databaseConnection.CreateCommand();

            var encryptedUsername = DataEncrypter.Crypt(username);
            var encryptedPassword = DataEncrypter.Crypt(password);
            
            string values = " VALUES ('" + encryptedUsername +"','" + encryptedPassword +"');";
            
            Debug.Log(values);
            
            string sqlQuery = "INSERT INTO LOGIN (username, password)" + values;

            databaseCommand.CommandText = sqlQuery;
            databaseCommand.ExecuteScalar();
            databaseConnection.Close();
        }
    }

    public List<Account> GetAccountsFromDB()
    {
        using (databaseConnection)
        {
            List<string> list = new List<string>();
            List<Account> accList = new List<Account>();
            databaseConnection.Open();
            databaseCommand = databaseConnection.CreateCommand();
            SqliteDataReader reader;

            string sqlQuery = "SELECT * FROM LOGIN";
                
            databaseCommand.CommandText = sqlQuery;
            reader = databaseCommand.ExecuteReader();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    list.Add(reader.GetValue(i).ToString());
                }
                
            }

            for (int i = 0; i < list.Count-3; i+=3)
            {
                string username = DataEncrypter.Decrypt(list[i + 1]);
                string password = DataEncrypter.Decrypt(list[i + 2]);
                accList.Add(new Account(int.Parse(list[i]), username,password));
            }

            databaseConnection.Close();
            
            /*Debug.Log("Printing from DB:");
            Print(accList);
            Debug.Log("Printing DONE");*/

            return accList;

        }
    }
    
    void Print(List<Account> accounts)
    {
        foreach (var acc in accounts)
        {
            Debug.Log(acc.ToString());
        }
    }

}
