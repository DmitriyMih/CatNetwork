using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Toggle))]
public class HidePasswordToggle : MonoBehaviour
{
    private Toggle toggle;
    [SerializeField] private TMP_InputField passwordInputField;

    [SerializeField] private bool isHide;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();

        if (toggle != null)
        {
            toggle.onValueChanged.AddListener((value) => OnToggleChanged(value));
            OnToggleChanged(toggle.isOn);
        }
        else Debug.Log("Button Text " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
    }

    private void OnToggleChanged(bool isOn)
    {
        isHide = isOn;

        if (passwordInputField != null)
        {
            passwordInputField.contentType = !isHide ? TMP_InputField.ContentType.Password : TMP_InputField.ContentType.Standard;
            passwordInputField.ForceLabelUpdate();
        }
    }
}