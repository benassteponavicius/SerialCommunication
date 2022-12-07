using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogOut : MonoBehaviour
{
    public void LoadLogin()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
