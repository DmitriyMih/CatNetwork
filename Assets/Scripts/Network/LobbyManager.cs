using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public enum LobbyType
{
    Host,
    Joined
}

public class LobbyManager : Singleton<LobbyManager>
{
    public const string LobbyMapConst = "Map";
    public const string PlayerNameConst = "PlayerName";

    [SerializeField] private Lobby activeLobby;
    [SerializeField] private LobbyType currentLobbyType;

    private float heartbeatTimerMax = 15f;
    private float heartbeatTimer;

    private float lobbyUpdateTimerMax = 1.1f;
    private float lobbyUpdateTimer;

    [SerializeField] private string tempPlayerNameInLobby;
    [SerializeField] private string tempLobbyCode;
    [SerializeField] private Dictionary<int, string> keys;
    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId % Colorize.Green % FontFormat.Bold);
        };

        tempPlayerNameInLobby = "Player " + Random.Range(10, 99);
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void Update()
    {
        HandleLobbyHeartbeat(); 
        HandleLobbyPollForUpdates();
    }

    private async void HandleLobbyHeartbeat()
    {
        if (activeLobby == null)
            return;

        if (currentLobbyType != LobbyType.Host)
            return;

        if (heartbeatTimer <= 0)
        {
            heartbeatTimer = heartbeatTimerMax;
            await LobbyService.Instance.SendHeartbeatPingAsync(activeLobby.Id);
        }
        else
            heartbeatTimer -= Time.deltaTime;
    }

    //  Update Lobby
    private async void HandleLobbyPollForUpdates()
    {
        if (activeLobby == null)
            return;

        if (currentLobbyType != LobbyType.Joined)
            return;

        if (lobbyUpdateTimer <= 0)
        {
            lobbyUpdateTimer = lobbyUpdateTimerMax;
            Lobby lobby = await LobbyService.Instance.GetLobbyAsync(activeLobby.Id);
            activeLobby = lobby;
        }
        else
            lobbyUpdateTimer -= Time.deltaTime;
    }

    private async void StopLobby()
    {
        if (activeLobby == null)
            return;

        await LobbyService.Instance.DeleteLobbyAsync(activeLobby.Id);
        Debug.Log("Stop Lobby" % Colorize.DarkOrange % FontFormat.Bold);
    }

    public async void CreateLobby(LobbySettingsSO lobbySettingsSO)
    {
        try
        {
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = !lobbySettingsSO.isPublic,
                Player = GetPlayer(),
                Data = new Dictionary<string, DataObject>
                {
                    { LobbyMapConst, new DataObject(DataObject.VisibilityOptions.Public, "First_Map", DataObject.IndexOptions.S1)}
                }
            };

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbySettingsSO.lobbyName, lobbySettingsSO.playersMaxCount, createLobbyOptions);
            Debug.Log($"Create Lobby! {lobby.Name} | Map {lobby.Data[LobbyMapConst].Value} | Players Max { lobby.MaxPlayers} | Lobby ID: {lobby.Id} | Lobby Code {lobby.LobbyCode}" % Colorize.Green % FontFormat.Bold);

            if (activeLobby != null)
                StopLobby();

            activeLobby = lobby;
            currentLobbyType = LobbyType.Host;

            PrintPlayers(activeLobby);
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
            Lobby lobby = await Lobbies.Instance.JoinLobbyByIdAsync(queryResponse.Results[0].Id);

            if (activeLobby != null)
                StopLobby();

            activeLobby = lobby;
            currentLobbyType = LobbyType.Joined;

            Debug.Log($"Joined Lobby: {tempLobbyCode}" % Colorize.Green % FontFormat.Bold);
            PrintPlayers(lobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    [ContextMenu("Lobby Manager / Join Lobbies")]
    private void JoinByCode()
    {
        JoinLobbyByCode(tempLobbyCode);
    }

    public async void JoinLobbyByCode(string lobbyCode)
    {
        try
        {
            JoinLobbyByCodeOptions joinLobbyByCodeOptions = new JoinLobbyByCodeOptions
            {
                Player = GetPlayer()
            };
            Lobby lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode, joinLobbyByCodeOptions);

            if (activeLobby != null)
                StopLobby();

            activeLobby = lobby;
            currentLobbyType = LobbyType.Joined;

            Debug.Log($"Joined Lobby With Code: {lobbyCode}" % Colorize.Green % FontFormat.Bold);
            PrintPlayers(lobby);
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
            Data = new Dictionary<string, PlayerDataObject>
            {
                {PlayerNameConst, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, tempPlayerNameInLobby) }
            }
        };
    }

    [ContextMenu("Lobby Manager / Show List Lobbies")]
    public async void ShowListLobbies()
    {
        try
        {
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            Debug.Log("Lobbies Found: " + queryResponse.Results.Count.ToString() % Colorize.Yellow % FontFormat.Bold);
            for (int i = 0; i < queryResponse.Results.Count; i++)
            {
                Debug.Log($"{i + 1}: Lobby Name: {queryResponse.Results[i].Name} | Lobby Mode {queryResponse.Results[i].Data["GameMode"].Value}| " % Colorize.Yellow % FontFormat.Bold + $"Players: {queryResponse.Results[i].Players.Count}/{queryResponse.Results[i].MaxPlayers}" % Colorize.Green % FontFormat.Bold);
                PrintPlayers(queryResponse.Results[i]);
            }
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
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT)
                    //new QueryFilter(QueryFilter.FieldOptions.S1, "First_Map", QueryFilter.OpOptions.EQ)
                },
                Order = new List<QueryOrder>
                {
                new QueryOrder(false, QueryOrder.FieldOptions.Created)
                }
            };

            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync(queryLobbiesOptions);

            Debug.Log("Lobbies Found: " + queryResponse.Results.Count.ToString() % Colorize.Yellow % FontFormat.Bold);
            for (int i = 0; i < queryResponse.Results.Count; i++)
            {
                Debug.Log($"{i + 1}: Lobby Name: {queryResponse.Results[i].Name} | Lobby Mode {queryResponse.Results[i].Data[LobbyMapConst].Value}| " % Colorize.Yellow % FontFormat.Bold + $"Players: {queryResponse.Results[i].Players.Count}/{queryResponse.Results[i].MaxPlayers}" % Colorize.Green % FontFormat.Bold);
                PrintPlayers(queryResponse.Results[i]);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public void PrintPlayers()
    {
        if (activeLobby == null)
            return;

        PrintPlayers(activeLobby);
    }

    private void PrintPlayers(Lobby lobby)
    {
        Debug.Log($"Players In Lobby: {lobby.Name} | Players: {lobby.Players.Count} | Game Map: {lobby.Data[LobbyMapConst].Value} | Lobby Code: {lobby.LobbyCode}" % Colorize.Orange % FontFormat.Bold);
        for (int i = 0; i < lobby.Players.Count; i++)
        {
            Player player = lobby.Players[i];
            Debug.Log($"{i + 1}: Player Name: {player.Data[PlayerNameConst].Value} | " % Colorize.Yellow % FontFormat.Bold + $"Player Id: {player.Id}" % Colorize.Green % FontFormat.Bold);
        }
    }

    [ContextMenu("Update Player")]
    private void UpdatePlayer()
    {
        UpdatePlayerName(tempPlayerNameInLobby);
    }

    public async void UpdatePlayerName(string newPlayerName)
    {
        try
        {
            if (newPlayerName == "")
                return;

            if (activeLobby == null)
                return;

            Debug.Log($"Old Name: {tempPlayerNameInLobby} | " % Colorize.Orange % FontFormat.Bold + $"New Name: {newPlayerName}" % Colorize.Yellow % FontFormat.Bold);
            Debug.Log($"Lobby: {activeLobby.Name} | Lobby ID: {activeLobby.Id}" % Colorize.Green % FontFormat.Bold);
            Debug.Log($"Player ID: {AuthenticationService.Instance.PlayerId}" % Colorize.Orange % FontFormat.Bold);
            tempPlayerNameInLobby = newPlayerName;

            UpdatePlayerOptions options = new UpdatePlayerOptions();
            options.Data = new Dictionary<string, PlayerDataObject>()
            {
                {PlayerNameConst, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, newPlayerName)},
            };

            Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(activeLobby.Id, AuthenticationService.Instance.PlayerId, options);
            PrintPlayers(lobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}