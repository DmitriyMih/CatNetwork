using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class AccessToggle : MonoBehaviour
{
    private Button toggleButton;
    private TextMeshProUGUI buttonText;

    [SerializeField] private bool isPublic;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Navigation")]
    [SerializeField] private Button firstButton;
    [SerializeField] private Button secondButton;

    [SerializeField] private Selectable analogSelectPath;

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

        if (canvasGroup != null)
        {
            canvasGroup.alpha = isPublic ? 1f : 0.5f;
            canvasGroup.interactable = isPublic;
        }

        UpdateNavigation();
    }

    private void UpdateNavigation()
    {
        if (firstButton == null || secondButton == null || analogSelectPath == null)
            return;

        Navigation navigationA = firstButton.navigation;
        navigationA.selectOnDown = isPublic ? analogSelectPath : secondButton;
        firstButton.navigation = navigationA;

        Navigation navigationB = secondButton.navigation;
        navigationB.selectOnUp = isPublic ? analogSelectPath : firstButton;
        secondButton.navigation = navigationB;
    }
}