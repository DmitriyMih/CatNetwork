using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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

    private Coroutine lobbyNameInputCorrutine;
    private Coroutine lobbyPasswordCorrutine;

    [SerializeField] private float fadeTime = 1f;

    [SerializeField] private TMP_InputField lobbyNameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    [Header("Lobby Data"), Space()]
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
                if (lobbyNameInputCorrutine != null)
                    StopCoroutine(lobbyNameInputCorrutine);

                lobbyNameInputCorrutine = StartCoroutine(FadeNotice(lobbyNameInputNotice, fadeTime));
                isChecks = false;
            }

            Debug.Log(outString);
        }
        else
        {
            Debug.Log("Lobby Name Field " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
            isChecks = false;
        }

        bool showPassword = false;
        if (accessToggle != null)
        {
            showPassword = accessToggle.GetAccessState();
            lobbySettingsSO.isPublic = showPassword;
        }
        else
        {
            Debug.Log("Players Toggle " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
            isChecks = false;
        }

        Debug.Log("Show Password: " + showPassword);
        if (!showPassword)
        {
            Debug.Log("Check");
            if (passwordInputField != null)
            {
                string outPassword = passwordInputField.text;
                if (outPassword != "")
                    lobbySettingsSO.password = outPassword;
                else
                {
                    if (lobbyPasswordCorrutine != null)
                        StopCoroutine(lobbyPasswordCorrutine);

                    lobbyPasswordCorrutine = StartCoroutine(FadeNotice(lobbyPasswordInputNotice, fadeTime));
                    Debug.Log("Fade Second");
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

    private IEnumerator FadeNotice(CanvasGroup canvasGroup, float fadeTime)
    {
        canvasGroup.alpha = 1f;
        float coef = 1f / (fadeTime * Time.deltaTime);
        yield return new WaitForSeconds(fadeTime);

        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= coef;
        }
    }
}