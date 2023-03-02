using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu()]
public class LobbySettingsSO : ScriptableObject
{
    public string lobbyName;
    public string ownerName;
    public int playersMaxCount;

    public bool isPublic;
    public string password;
}