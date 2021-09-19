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
        Map.Instance.Create();
        Map.Instance.InitializeTowers();
        Map.Instance.RefillTiles();
        EnemyManager.Instance.Create();

        if (SceneManager.GetActiveScene().name == SceneHandler.sceneCard)
        {
            CardManager.Instance.RefreshHandCards();
        }
    }

    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        // Debug.Log("RuntimeMethodLoad: After first Scene loaded");

    }
}
