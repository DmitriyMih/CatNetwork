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
    [SerializeField] private BaseLobbyPanel passwordPanel;
    [SerializeField] private BaseLobbyPanel lobbyPanel;

    private void Start()
    {
        LoadPanel(LobbyPanel.LobbyListPanel);
    }

    public void LoadPanel(LobbyPanel lobbyPanels)
    {
        Hide();

        switch (lobbyPanels)
        {
            case LobbyPanel.LobbyListPanel:
                if (lobbyListPanel == null)
                    return;

                lobbyListPanel.ShowPanel();
                break;

            case LobbyPanel.CreateLobbyPanel:
                if (createLobbyPanel == null)
                    return;

                createLobbyPanel.ShowPanel();
                break;

            case LobbyPanel.PasswordPanel:
                if (passwordPanel == null)
                    return;

                passwordPanel.ShowPanel();
                break;

            case LobbyPanel.LobbyPanel:
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

public enum LobbyPanel
{
    LobbyListPanel,
    CreateLobbyPanel,
    PasswordPanel,
    LobbyPanel
}