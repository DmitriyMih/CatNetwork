using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class LobbyManager : Singleton<LobbyManager>
{
    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId % Colorize.Green % FontFormat.Bold);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    [ContextMenu("Lobby Manager / Create Lobby")]
    public async void CreateLobby()
    {
        try
        {
            string lobbyName = "MyLobby";
            int maxPlayers = 4;

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);
            Debug.Log("Create Lobby! " + lobby.Name + " | " + lobby.MaxPlayers);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    [ContextMenu("Lobby Manager / List Lobbies")]
    public async void ListLobbies()
    {
        try
        {
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            Debug.Log("Lobbies Found: " + queryResponse.Results.Count.ToString() % Colorize.Yellow % FontFormat.Bold);
            for (int i = 0; i < queryResponse.Results.Count; i++)
                Debug.Log($"{i + 1}: Lobby Name: {queryResponse.Results[i].Name} | " % Colorize.Yellow % FontFormat.Bold + $"Players: {queryResponse.Results[i].Players.Count}/{queryResponse.Results[i].MaxPlayers}" % Colorize.Green % FontFormat.Bold);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}