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
    Gameplay
}

public class LoadingSceneManager : Singleton<LoadingSceneManager>
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
        }
    }

    private void Load(int index)
    {
        if (index >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log($"Scene At: {index} | " % Colorize.Yellow % FontFormat.Bold + $"Not Found" % Colorize.Red % FontFormat.Bold);
            return;
        }

        SceneManager.LoadScene(index);
    }
}