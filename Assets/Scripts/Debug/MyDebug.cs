using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyDebug : MonoBehaviour
{
    [SerializeField] private Button joinByCodeButton;
    [SerializeField] private Button setNewNameInLobbyButton;
    [SerializeField] private Button showAllLobbiesButton;
    [SerializeField] private Button printPlayersButton;

    [Space(15)]
    [SerializeField] private TMP_InputField inputCodeField;
    [SerializeField] private TMP_InputField inputNameField;

    [Space(15)]
    [SerializeField] private string tempLobbyCode;
    [SerializeField] private string tempPlayerNameInLobby;

    private void Awake()
    {
        if (inputCodeField != null)
            inputCodeField.onEndEdit.AddListener((value) => SetLobbyCode(value));
      
        if (inputNameField != null)
            inputNameField.onEndEdit.AddListener((value) => SetLobbyName(value));

        if (joinByCodeButton != null)
            joinByCodeButton.onClick.AddListener(() => LobbyManager.Instance.JoinLobbyByCode(tempLobbyCode));
        
        if (setNewNameInLobbyButton != null)
            setNewNameInLobbyButton.onClick.AddListener(() => LobbyManager.Instance.UpdatePlayerName(tempPlayerNameInLobby));

        if (showAllLobbiesButton != null)
            showAllLobbiesButton.onClick.AddListener(() => LobbyManager.Instance.ShowListFiltersLobbies());

        if (printPlayersButton != null)
            printPlayersButton.onClick.AddListener(() => LobbyManager.Instance.PrintPlayers());
    }

    private void SetLobbyCode(string newCode)
    {
        tempLobbyCode = newCode.ToUpper();
    }  
    
    private void SetLobbyName(string newName)
    {
        tempPlayerNameInLobby = newName;
    }
}