using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyDebug : MonoBehaviour
{
    [SerializeField] private Button joinByCode;
    [SerializeField] private Button showAllLobbies;
    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private Button printPlayersButton;

    [SerializeField] private string lobbyCode;

    private void Awake()
    {
        if (joinByCode != null)
            joinByCode.onClick.AddListener(() => LobbyManager.Instance.JoinLobbyByCode(lobbyCode));

        if (inputField != null)
            inputField.onEndEdit.AddListener((value) => SetLobbyCode(value));

        if (showAllLobbies != null)
            showAllLobbies.onClick.AddListener(() => LobbyManager.Instance.ShowListFiltersLobbies());

        if (printPlayersButton != null)
            printPlayersButton.onClick.AddListener(() => LobbyManager.Instance.PrintPlayers());
    }

    private void SetLobbyCode(string newCode)
    {
        lobbyCode = newCode.ToUpper();
    }
}