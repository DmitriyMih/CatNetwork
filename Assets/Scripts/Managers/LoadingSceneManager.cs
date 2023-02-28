using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName : byte
{
    NextScene,
    Menu,
    LobbySelection,
    CharacterSelection,
    Gameplay
}

public class LoadingSceneManager : SingletonPersistent<LoadingSceneManager>
{
    public void LoadScene(SceneName sceneName)
    {
        switch (sceneName)
        {
            case SceneName.NextScene:
                int currentIndex = SceneManager.GetActiveScene().buildIndex;
                Load(currentIndex);
                break;

            case SceneName.Menu:
                Load(0);
                break;

            case SceneName.LobbySelection:
                Load(1);
                break;

            case SceneName.CharacterSelection:
                Load(2);
                break;
        }
    }

    private void Load(int index)
    {
        if (SceneManager.GetSceneAt(index).IsValid())
        {
            Debug.Log($"Scene At: {index} | " % Colorize.Yellow % FontFormat.Bold + $"Not Found" % Colorize.Red % FontFormat.Bold);
            return;
        }

        SceneManager.LoadScene(index);
    }
}