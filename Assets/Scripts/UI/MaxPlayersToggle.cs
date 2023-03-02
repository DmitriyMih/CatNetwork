using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaxPlayersToggle : MonoBehaviour
{
    private Button toggleButton;
    private TextMeshProUGUI playersCountText;

    [SerializeField, Range(2, 4)] private int maxPlayersCount = 4;
    [SerializeField] private int currentPlayersCount;

    private void Awake()
    {
        toggleButton = GetComponent<Button>();
        playersCountText = GetComponentInChildren<TextMeshProUGUI>();

        if (toggleButton != null)
            toggleButton.onClick.AddListener(() => ChangeValue());
        else Debug.Log("Toggle Button " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);

        currentPlayersCount = maxPlayersCount;
        UpdateInfoText();
    }

    private void ChangeValue()
    {
        if (currentPlayersCount >= maxPlayersCount)
            currentPlayersCount = 2;
        else
            currentPlayersCount += 1;

        UpdateInfoText();
    }

    private void UpdateInfoText()
    {
        if (playersCountText != null)
            playersCountText.text = $"{currentPlayersCount}";
        else Debug.Log("Players Count Text " % Colorize.Yellow % FontFormat.Bold + "| Is Null |" % Colorize.Red % FontFormat.Bold);
    }

    public int GetMaxPlayersCount()
    {
        return currentPlayersCount;
    }
}