using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class LobbyManager : Singleton<LobbyManager>
{
    private Lobby hostLobby;
    private float heartbeatTimerMax = 15f;
    private float heartbeatTimer;

    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId % Colorize.Green % FontFormat.Bold);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void Update()
    {
        HandleLobbyHeartbeat();
    }

    private async void HandleLobbyHeartbeat()
    {
        if (hostLobby != null)
        {
            if (heartbeatTimer <= 0)
            {
                heartbeatTimer = heartbeatTimerMax;

                await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
            else
                heartbeatTimer -= Time.deltaTime;
        }
    }

    //[ContextMenu("Lobby Manager / Create Lobby")]
    public async void CreateLobby(LobbySettingsSO lobbySettingsSO)
    {
        try
        {
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbySettingsSO.lobbyName, lobbySettingsSO.playersMaxCount);
            Debug.Log("Create Lobby! " + lobby.Name + " | " + lobby.MaxPlayers);

            hostLobby = lobby;
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    [ContextMenu("Lobby Manager / Show List Lobbies")]
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

    [ContextMenu("Lobby Manager / Show Filters List Lobbies")]
    public async void ListFiltersLobbies()
    {
        try
        {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
            {
                Count = 25,
                Filters = new List<QueryFilter> { new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT) },
                Order = new List<QueryOrder>
                {
                new QueryOrder(false, QueryOrder.FieldOptions.Created)
                }
            };

            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync(queryLobbiesOptions);

            Debug.Log("Lobbies Found: " + queryResponse.Results.Count.ToString() % Colorize.Yellow % FontFormat.Bold);
            for (int i = 0; i < queryResponse.Results.Count; i++)
                Debug.Log($"{i + 1}: Lobby Name: {queryResponse.Results[i].Name} | " % Colorize.Yellow % FontFormat.Bold + $"Players: {queryResponse.Results[i].Players.Count}/{queryResponse.Results[i].MaxPlayers}" % Colorize.Green % FontFormat.Bold);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    [ContextMenu("Lobby Manager / Join Lobbies")]
    private async void Joinlobby()
    {
        try
        {
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();
            await Lobbies.Instance.JoinLobbyByIdAsync(queryResponse.Results[0].Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}