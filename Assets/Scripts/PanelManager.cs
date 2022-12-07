using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject registerPanel;
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private TMP_Dropdown port;
    [SerializeField] private TMP_Dropdown baudRate;

    private bool LoginPanelIsOpen = true;
    private bool SettingsPanelIsOpen = false;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            if (LoginPanelIsOpen)
            {
                loginPanel.SetActive(false);
                registerPanel.SetActive(true);
                LoginPanelIsOpen = false;
            }
            else
            {
                loginPanel.SetActive(true);
                registerPanel.SetActive(false);
                LoginPanelIsOpen = true;
            }
        }
    }

    public void SettingsPanel()
    {
        if (SettingsPanelIsOpen)
        {
            settingsPanel.SetActive(false);
        }
        else
        {
            settingsPanel.SetActive(true);
            SettingsPanelIsOpen = true;
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetString("Port", port.options[port.value].text);
        PlayerPrefs.SetInt("BaudRate", int.Parse(baudRate.options[baudRate.value].text));
        
        Debug.Log($"Port: {PlayerPrefs.GetString("Port")}  Baud Rate: {PlayerPrefs.GetInt("BaudRate")}");
    }
}
