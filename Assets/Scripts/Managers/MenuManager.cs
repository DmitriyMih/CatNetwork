using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button lobbyButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        if (lobbyButton != null)
            lobbyButton.onClick.AddListener(() => OnClickLobby());
        else Debug.Log("Lobby Button " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
             
        if (quitButton != null)
            quitButton.onClick.AddListener(() => Quit());
        else Debug.Log("Quit Button " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
    }

    private void OnClickLobby()
    {
        LoadingSceneManager.Instance.LoadScene(SceneName.LobbySelection);
    }

    private void Quit()
    {
        Application.Quit();
    }
}