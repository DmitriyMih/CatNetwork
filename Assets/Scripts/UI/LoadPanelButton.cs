using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadPanelButton : MonoBehaviour
{
    protected Button loadButton;
    [SerializeField] private LobbyPanel nextLobbyPanel;

    protected virtual void Awake()
    {
        loadButton = GetComponent<Button>();

        if (loadButton != null)
            loadButton.onClick.AddListener(() => LobbyPanelsManager.Instance.LoadPanel(nextLobbyPanel));
        else Debug.Log("Load Button " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
    }
}