using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseLobbyPanel : MonoBehaviour
{
    [SerializeField] private Selectable firstSelectable;

    protected virtual void Awake()
    {
        HidePanel();
    }

    public virtual void ShowPanel()
    {
        LobbyPanelsManager.Instance.HideAction = HidePanel;

        gameObject.SetActive(true);

        if (firstSelectable != null)
            firstSelectable.Select();
    }

    protected virtual void HidePanel()
    {
        gameObject.SetActive(false);
    }
}