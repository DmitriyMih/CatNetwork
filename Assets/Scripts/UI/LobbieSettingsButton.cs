using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbieSettingsButton : LoadPanelButton
{
    [SerializeField] private LobbieSettingsUI lobbieSettingsUI;

    protected override void Awake()
    {
        base.Awake();
        loadButton.onClick.AddListener(() => DefaultSettings());
    }

    private void DefaultSettings()
    {
        if (lobbieSettingsUI != null)
            lobbieSettingsUI.PanelToDefaultClick();
    }
}