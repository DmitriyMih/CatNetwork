using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName : byte
{
    Menu,
    CharacterSelection,
    Gameplay
}

public class LoadingSceneManager : SingletonPersistent<LoadingSceneManager>
{
    public void loadScene(SceneName sceneName)
    {

    }
}