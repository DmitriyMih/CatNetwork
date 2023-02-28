using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MenuManager : MonoBehaviour
{
    public void OnClickHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void OnClickJoin()
    {
        NetworkManager.Singleton.StartHost();
    }
}