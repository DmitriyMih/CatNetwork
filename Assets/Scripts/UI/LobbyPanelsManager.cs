using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPanelsManager : Singleton<LobbyPanelsManager>
{
    public Action HideAction;

    [Space(15)]
    [SerializeField] private BaseLobbyPanel lobbyListPanel;
    [SerializeField] private BaseLobbyPanel createLobbyPanel;
    [SerializeField] private BaseLobbyPanel lobbyPanel;

    private void Start()
    {
        LoadPanel(LobbyPanels.LobbyListPanel);
    }

    public void LoadPanel(LobbyPanels lobbyPanels)
    {
        Hide();

        switch (lobbyPanels)
        {
            case LobbyPanels.LobbyListPanel:
                if (lobbyListPanel == null)
                    return;

                lobbyListPanel.ShowPanel();
                break;

            case LobbyPanels.CreateLobbyPanel:
                if (createLobbyPanel == null)
                    return;

                createLobbyPanel.ShowPanel();
                break;

            case LobbyPanels.Lobby:
                if (lobbyPanel == null)
                    return;

                lobbyPanel.ShowPanel();
                break;
        }
    }

    private void Hide()
    {
        if (HideAction != null)
        {
            HideAction();
            HideAction = null;
        }
    }
}

public enum LobbyPanels
{
    LobbyListPanel,
    CreateLobbyPanel,
    Lobby
}