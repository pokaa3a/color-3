using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// The first script when the scene is loaded
class Starter
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        // Debug.Log("Before first Scene loaded");

    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        // Debug.Log("After first Scene loaded");
        GameStats.Instance.score = 0;
        Map.Instance.InitializeMap();
        EnemyManager.Instance.Summon();

        if (SceneManager.GetActiveScene().name == SceneHandler.sceneCard)
        {
            CardManager.Instance.RefreshHandCards();
        }
        else if (SceneManager.GetActiveScene().name == SceneHandler.sceneCharacter)
        {
            CharacterManager.Instance.Summon();
        }
    }

    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        // Debug.Log("RuntimeMethodLoad: After first Scene loaded");

    }
}
