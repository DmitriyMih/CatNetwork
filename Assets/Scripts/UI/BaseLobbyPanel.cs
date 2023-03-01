using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseLobbyPanel : MonoBehaviour
{
    [SerializeField] private Selectable firstSelectable;
    [SerializeField] private GameObject content;

    protected virtual void Awake()
    {
        content = transform.GetChild(0).gameObject;
        HidePanel();
    }

    public virtual void ShowPanel()
    {
        if (LobbyPanelsManager.Instance != null)
            LobbyPanelsManager.Instance.HideAction = HidePanel;

        content.SetActive(true);

        if (firstSelectable != null)
            firstSelectable.Select();
    }

    protected virtual void HidePanel()
    {
        content.SetActive(false);
    }
}