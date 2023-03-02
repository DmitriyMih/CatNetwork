using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbieSettingsUI : MonoBehaviour
{
    [SerializeField] private Button createLobbyButton;

    [Header("Access Settings")]
    [SerializeField] private AccessToggle accessToggle;

    [Header("Players Count Settings")]
    [SerializeField] private MaxPlayersToggle playersToggle;

    [Header("Input Fild Settings")]
    [SerializeField] private CanvasGroup lobbyNameInputNotice;
    [SerializeField] private CanvasGroup lobbyPasswordInputNotice;

    [SerializeField] private float fadeTime = 1f;

    [SerializeField] private TMP_InputField lobbyNameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    [Header("Lobby Data"), Space()]
    [SerializeField] private string lobbyName;
    [SerializeField] private bool isPunlic;
    [SerializeField] private string playersMaxCount;
    [SerializeField] private string ownerName;

    [SerializeField] private LobbySettingsSO lobbySettings;

    private void Awake()
    {
        if (createLobbyButton != null)
            createLobbyButton.onClick.AddListener(() => CreateLobbySettingsSO());
        else Debug.Log("Create Button " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
    }

    private void CreateLobbySettingsSO()
    {
        bool isChecks = true;
        LobbySettingsSO lobbySettingsSO = new LobbySettingsSO();

        if (lobbyNameInputField != null)
        {
            string outString = lobbyNameInputField.text;
            if (outString != "")
                lobbySettingsSO.lobbyName = outString;
            else
            {
                StartCoroutine(NoticeFade(lobbyPasswordInputNotice));
                isChecks = false;
            }

            Debug.Log(outString);
        }
        else
        {
            Debug.Log("Lobby Name Field " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
            isChecks = false;
        }

        if (accessToggle != null)
        {
            bool isPublic = accessToggle.GetAccessState();
            lobbySettingsSO.isPublic = isPublic;

            if (!isPublic)
            {
                if (passwordInputField != null)
                {
                    string outPassword = passwordInputField.text;
                    if (outPassword != "")
                        lobbySettingsSO.password = outPassword;
                    else
                    {
                        StartCoroutine(NoticeFade(lobbyPasswordInputNotice));
                        isChecks = false;
                    }

                    Debug.Log(outPassword);
                }
                else
                {
                    Debug.Log("Password Field " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
                    isChecks = false;
                }
            }
        }
        else
        {
            Debug.Log("Players Toggle " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
            isChecks = false;
        }

        if (playersToggle != null)
            lobbySettingsSO.playersMaxCount = playersToggle.GetMaxPlayersCount();
        else
        {
            Debug.Log("Players Toggle " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
            isChecks = false;
        }

        lobbySettings = lobbySettingsSO;
        Debug.Log("Is Check: " + isChecks);
    }

    private IEnumerator NoticeFade(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1f;
        yield return new WaitForSeconds(fadeTime);
    }
}