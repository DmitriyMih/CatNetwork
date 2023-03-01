using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinButtonController : MonoBehaviour
{
    [SerializeField] private Button quitButton;

    [SerializeField] private Button joinButton;
    [SerializeField] private Button leaveButton;

    [SerializeField] private CanvasGroup joinCanvasGroup;
    [SerializeField] private CanvasGroup leaveCanvasGroup;

    [SerializeField] private bool isJoin;

    private void Awake()
    {
        if (quitButton != null)
            quitButton.onClick.AddListener(() => Default());

        if (joinButton != null)
            joinButton.onClick.AddListener(() => OnJoinClicked());
        
        if (leaveButton != null)
            leaveButton.onClick.AddListener(() => OnJoinClicked());

        UpdateUI();
    }

    private void Default()
    {
        isJoin = false;
        UpdateUI();
    }

    private void OnJoinClicked()
    {
        isJoin = !isJoin;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (leaveCanvasGroup == null || joinCanvasGroup == null)
            return;

        joinCanvasGroup.alpha = !isJoin ? 1f : 0.5f;
        leaveCanvasGroup.alpha = isJoin ? 1f : 0.5f;

        joinCanvasGroup.interactable = !isJoin;
        leaveCanvasGroup.interactable = isJoin;
    }
}