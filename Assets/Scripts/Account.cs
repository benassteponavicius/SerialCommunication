using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account
{
    private int ID;
    private string username;
    private string password;

    public Account()
    {
        ID = 0;
        username = "";
        password = "";
    }
    
    public Account(int ID, string username, string password)
    {
        this.ID = ID;
        this.username = username;
        this.password = password;
    }

    public string GetUsername()
    {
        return username;
    }
    
    public string GetPassword()
    {
        return password;
    }

    public override string ToString()
    {
        return $"ID: {ID}  USERNAME: {username}  PASS: {password}";
    }
}