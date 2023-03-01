using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPanelsManager : Singleton<LobbyPanelsManager>
{
    public Action HideAction;

    [SerializeField] private BaseLobbyPanel lobbyListPanel;
    [SerializeField] private BaseLobbyPanel createLobbyPanel;
    [SerializeField] private BaseLobbyPanel lobbyPanel;

    public override void Awake()
    {
        base.Awake();
        LoadPanel(LobbyPanels.LobbyListPanel);
    }

    public void LoadPanel(LobbyPanels lobbyPanels)
    {
        switch (lobbyPanels)
        {
            case LobbyPanels.LobbyListPanel:
                if (lobbyListPanel == null)
                    return;

                if (HideAction != null)
                {
                    HideAction();
                    HideAction = null;
                }

                lobbyListPanel.ShowPanel();
                break;

            case LobbyPanels.CreateLobbyPanel:
                if (createLobbyPanel == null)
                    return;

                if (HideAction != null)
                {
                    HideAction();
                    HideAction = null;
                }

                createLobbyPanel.ShowPanel();
                break;
        }
    }
}

public enum LobbyPanels
{
    LobbyListPanel,
    CreateLobbyPanel
}