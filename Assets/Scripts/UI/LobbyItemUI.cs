using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private TextMeshProUGUI playersCountText;
    [SerializeField] private TextMeshProUGUI ownerNameText;

    [SerializeField] private int playersMaxCount;

    public void Initialization(string lobbyName, string ownerName, int maxCount)
    {
        playersMaxCount = maxCount;

        if (lobbyNameText != null)
            lobbyNameText.text = lobbyName;
        else Debug.Log("Lobby Name Text " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
     
        if (ownerNameText != null)
            ownerNameText.text = lobbyName;
        else Debug.Log("Owner Name Text " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
    }

    public void UpdatePlayersCount(int newValue)
    {
        if (playersCountText != null)
            playersCountText.text = $"{newValue}/{playersMaxCount}";
        else Debug.Log("Players Count Text " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
    }
}