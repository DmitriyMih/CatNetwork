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

    [SerializeField] private string playerName;

    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId % Colorize.Green % FontFormat.Bold);
        };

        playerName = "Player " + Random.Range(10, 99);
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

    public async void CreateLobby(LobbySettingsSO lobbySettingsSO)
    {
        try
        {
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = lobbySettingsSO.isPublic,
                Player = GetPlayer(),
                Data = new Dictionary<string, DataObject>
                {
                    { "GameMode", new DataObject(DataObject.VisibilityOptions.Public, "FastMode", DataObject.IndexOptions.S1)}
                }
            };

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbySettingsSO.lobbyName, lobbySettingsSO.playersMaxCount, createLobbyOptions);
            Debug.Log($"Create Lobby! {lobby.Name} | Players Max { lobby.MaxPlayers} | Lobby ID: {lobby.Id} | Lobby Code {lobby.LobbyCode}" % Colorize.Green % FontFormat.Bold);

            hostLobby = lobby;
            PrintPlayers(hostLobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    [ContextMenu("Lobby Manager / Show List Lobbies")]
    public async void ShowListLobbies()
    {
        try
        {
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            Debug.Log("Lobbies Found: " + queryResponse.Results.Count.ToString() % Colorize.Yellow % FontFormat.Bold);
            for (int i = 0; i < queryResponse.Results.Count; i++)
                Debug.Log($"{i + 1}: Lobby Name: {queryResponse.Results[i].Name} | Lobby Mode {queryResponse.Results[i].Data["GameMode"].Value}| " % Colorize.Yellow % FontFormat.Bold + $"Players: {queryResponse.Results[i].Players.Count}/{queryResponse.Results[i].MaxPlayers}" % Colorize.Green % FontFormat.Bold);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    [ContextMenu("Lobby Manager / Show Filters List Lobbies")]
    public async void ShowListFiltersLobbies()
    {
        try
        {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
            {
                Count = 25,
                Filters = new List<QueryFilter>
                {
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT),
                    new QueryFilter(QueryFilter.FieldOptions.S1, "FastMode", QueryFilter.OpOptions.EQ)
                },
                Order = new List<QueryOrder>
                {
                new QueryOrder(false, QueryOrder.FieldOptions.Created)
                }
            };

            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync(queryLobbiesOptions);

            Debug.Log("Lobbies Found: " + queryResponse.Results.Count.ToString() % Colorize.Yellow % FontFormat.Bold);
            for (int i = 0; i < queryResponse.Results.Count; i++)
                Debug.Log($"{i + 1}: Lobby Name: {queryResponse.Results[i].Name} | Lobby Mode {queryResponse.Results[i].Data["GameMode"].Value}| " % Colorize.Yellow % FontFormat.Bold + $"Players: {queryResponse.Results[i].Players.Count}/{queryResponse.Results[i].MaxPlayers}" % Colorize.Green % FontFormat.Bold);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    [ContextMenu("Lobby Manager / Join Lobbies")]
    private async void JoinLobby()
    {
        try
        {
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();
            await Lobbies.Instance.JoinLobbyByIdAsync(queryResponse.Results[0].Id);

            Debug.Log("Joined Lobby");
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    [SerializeField] private string lobbyCode;
    [ContextMenu("Lobby Manager / Join Lobbies")]
    private void JoinByCode()
    {
        JoinLobbyByCode(lobbyCode);
    }

    public async void JoinLobbyByCode(string lobbyCode)
    {
        try
        {
            JoinLobbyByCodeOptions joinLobbyByCodeOptions = new JoinLobbyByCodeOptions
            {
                Player = GetPlayer()
            };
            Lobby joinedLobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode, joinLobbyByCodeOptions);

            Debug.Log($"Joined Lobby With Code: {lobbyCode}" % Colorize.Green % FontFormat.Bold);
            PrintPlayers(joinedLobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    [ContextMenu("Lobby Manager / Quick Join Lobby")]
    private async void QuickJoinLobby()
    {
        try
        {
            await LobbyService.Instance.QuickJoinLobbyAsync();
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private Player GetPlayer()
    {
        return new Player
        {
            Data = new Dictionary<string, PlayerDataObject> { { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName) } }
        };
    }

    private void PrintPlayers(Lobby lobby)
    {
        Debug.Log($"Players In Lobby: {lobby.Name} | Game Mode {lobby.Data["GameMode"].Value}");
        foreach (Player player in lobby.Players)
            Debug.Log(player.Id % Colorize.Yellow % FontFormat.Bold + " " + player.Data["PlayerName"].Value % Colorize.Green % FontFormat.Bold);
    }
}