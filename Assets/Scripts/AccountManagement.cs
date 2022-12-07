using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccountManagement : MonoBehaviour
{
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TMP_InputField usernameLOGIN;
    [SerializeField] private TMP_InputField passwordLOGIN;
    [SerializeField] private TMP_InputField repeatPass;
    [SerializeField] private DatabaseManager dbManager;
    
    [SerializeField] private TMP_Text errorField;
    [SerializeField] private TMP_Text errorFieldLOGIN;

    private bool readyToLogin;
    void Start()
    {
        readyToLogin = false;
        errorField.text = "";
        errorFieldLOGIN.text = "";
    }


    public void Login()
    {
        List<Account> accounts = dbManager.GetAccountsFromDB();
        
        errorFieldLOGIN.text = "";
        
        if (usernameLOGIN.text.Length < 1) errorFieldLOGIN.text += "Please write your username.";
        else if (passwordLOGIN.text.Length < 1) errorFieldLOGIN.text += "Please write your password.";
        else if (!UsernameExists(accounts, usernameLOGIN.text)) errorFieldLOGIN.text += "Username not found. ";
        else if (!AccountExists(accounts, usernameLOGIN.text, passwordLOGIN.text)) errorFieldLOGIN.text += "Incorrect password. ";

        if (AccountExists(accounts, usernameLOGIN.text, passwordLOGIN.text) && UsernameExists(accounts, usernameLOGIN.text)) readyToLogin = true;
        

        if (readyToLogin)
        {
            Debug.Log("Logging in...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }

    public void Register()
    {
        Debug.Log($"Username: {username.text} Pass: {password.text}");
        if (username.text.Length < 1) errorField.text = "Please write your username.";
        else if (password.text.Length < 1) errorField.text = "Please write your password.";
        else if (repeatPass.text.Length < 1) errorField.text = "Please repeat your password.";
        else if (password.text != repeatPass.text) errorField.text = "Passwords do not match.";
        else
        {
            Debug.Log("Account created successfully");
            dbManager.SendData(username.text, password.text);
        }
        
    }

    private bool AccountExists(List<Account> accounts, string user, string pass)
    {

        for (int i = 0; i < accounts.Count; i++)
        {
            if (user.Equals(accounts[i].GetUsername()))
            {
                if (pass.Equals(accounts[i].GetPassword()))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool UsernameExists(List<Account> accounts, string user)
    {
        for (int i = 0; i < accounts.Count; i++)
        {
            if (user.Equals(accounts[i].GetUsername()))
            {
                return true;
            }
        }

        return false;
    }

   
    
}
