using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private TextMeshProUGUI playersCountText;
    [SerializeField] private TextMeshProUGUI ownerNameText;

    [SerializeField] private int playersMaxCount;
    private Button enterButton;

    [Header("Panel Data")]
    [SerializeField] private LobbySettingsSO lobbySettingsSO;

    private void Awake()
    {
        enterButton = GetComponentInChildren<Button>();
    }

    public void InitializationPanel(LobbySettingsSO lobbySettingsSO)
    {
        this.lobbySettingsSO = lobbySettingsSO;

        if (lobbyNameText != null)
            lobbyNameText.text = lobbySettingsSO.lobbyName;
        else Debug.Log("Lobby Name Text " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
     
        if (ownerNameText != null)
            ownerNameText.text = lobbySettingsSO.ownerName;
        else Debug.Log("Owner Name Text " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);

        if (enterButton != null)
            enterButton.onClick.AddListener(() => EnterButtonOnClicked());
        else Debug.Log("Enter Button Action " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
    }

    public void UpdatePlayersCount(int newValue)
    {
        if (playersCountText != null)
            playersCountText.text = $"{newValue}/{playersMaxCount}";
        else Debug.Log("Players Count Text " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
    }

    private void EnterButtonOnClicked()
    {
        if (LobbyPanelsManager.Instance == null)
            return;

        LobbyPanel lobbyPanel = !lobbySettingsSO.isPublic ? LobbyPanel.PasswordPanel : LobbyPanel.LobbyPanel;
        LobbyPanelsManager.Instance.LoadPanel(lobbyPanel);
    }
}