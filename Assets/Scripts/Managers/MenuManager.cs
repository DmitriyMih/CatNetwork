using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button qutButton;

    private void OnClickHost()
    {

        NetworkManager.Singleton.StartHost();
    }

    private void OnClickJoin()
    {
        if (joinButton != null)
            NetworkManager.Singleton.StartClient();

    }

    private void Quit()
    {
        Application.Quit();
    }
}