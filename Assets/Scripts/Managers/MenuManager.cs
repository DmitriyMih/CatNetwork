using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        if (hostButton != null)
            hostButton.onClick.AddListener(() => OnClickHost());
        else Debug.Log("host Button " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
       
        if (joinButton != null)
            joinButton.onClick.AddListener(() => OnClickJoin());
        else Debug.Log("Joined Button " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
      
        if (quitButton != null)
            quitButton.onClick.AddListener(() => Quit());
        else Debug.Log("Quit Button " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
    }

    private void OnClickHost()
    {
        LobbyManager.Instance.CreateLobby();
    }

    private void OnClickJoin()
    {
        NetworkManager.Singleton.StartClient();
    }

    private void Quit()
    {
        Application.Quit();
    }
}