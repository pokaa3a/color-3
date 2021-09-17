using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Map.Instance.Create();
        Map.Instance.InitializeTowers();
        EnemyManager.Instance.Create();
    }

    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        // Debug.Log("RuntimeMethodLoad: After first Scene loaded");

    }
}
