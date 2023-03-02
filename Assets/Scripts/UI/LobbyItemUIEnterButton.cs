using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyItemUIEnterButton : MonoBehaviour
{
    [SerializeField] private LobbyItemUI parentItemUI;
    [SerializeField] private LobbySettingsSO lobbySettingsSO;

    [SerializeField] private Image buttonIcon;
    [SerializeField] private Sprite[] iconSprites;

    public void InitializationItem(LobbyItemUI newParentItemUI, LobbySettingsSO newLobbySettingsSO)
    {
        parentItemUI = newParentItemUI;
        lobbySettingsSO = newLobbySettingsSO;

        InitializationButton();
    }

    private void InitializationButton()
    {
        if (buttonIcon == null || lobbySettingsSO == null)
            return;

        switch(lobbySettingsSO.isPublic)
        {
            case false:
                if (iconSprites[0] != null)
                    buttonIcon.sprite = iconSprites[0];
                break;

            case true:
                if (iconSprites[1] != null)
                    buttonIcon.sprite = iconSprites[1];
                break;
        }
    }
}

public enum ButtonState
{
    Locked,
    Unlocked
}