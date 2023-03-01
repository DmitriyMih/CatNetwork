using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class AccessToggle : MonoBehaviour
{
    private Button toggleButton;
    private TextMeshProUGUI buttonText;

    [SerializeField] private bool isPublic;

    private void Awake()
    {
        toggleButton = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        if (toggleButton != null)
            toggleButton.onClick.AddListener(() => ChangeValue());
        else Debug.Log("Toggle Button " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);

        isPublic = true;
        UpdateInfoText();
    }

    private void ChangeValue()
    {
        isPublic = !isPublic; 
        UpdateInfoText();
    }

    private void UpdateInfoText()
    {
        if (buttonText != null)
            buttonText.text = isPublic ? "Public" : "Private";
        else Debug.Log("Button Text " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
    }
}