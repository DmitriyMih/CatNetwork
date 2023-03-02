using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyDebug : MonoBehaviour
{
    [SerializeField] private Button createLobbyButton;
    [SerializeField] private Button showLobbiesButton;

    private void Awake()
    {
        if (showLobbiesButton != null)
            showLobbiesButton.onClick.AddListener(() => LobbyManager.Instance.ListLobbies());
    }
}