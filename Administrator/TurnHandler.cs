using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

// This class handles everything related to switching turns.
public class TurnHandler
{
    // [Public]
    public bool playersTurn = true;

    // [Private]
    EndTurnButton endTurnButton;

    // Singleton
    private static TurnHandler _instance;
    public static TurnHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TurnHandler();
            }
            return _instance;
        }
    }

    private TurnHandler()
    {
        endTurnButton = GameObject.Find(
            ObjectPath.endTurnButton).GetComponent<EndTurnButton>() as EndTurnButton;
    }

    public void SwitchTurn()
    {
        if (playersTurn)    // player's turn -> enemy's turn
        {
            playersTurn = false;
            EnemyTurnStarts();
        }
        else                // enemy's turn -> player's turn
        {
            playersTurn = true;
            PlayerTurnStarts();
        }
    }

    public void PlayerTurnStarts()
    {
        endTurnButton.SetUnpressedSprite();
        Map.Instance.ResetTowers();
        if (SceneManager.GetActiveScene().name == SceneHandler.sceneCard)
        {
            CardManager.Instance.RefreshHandCards();
        }
    }

    public void EnemyTurnStarts()
    {
        endTurnButton.SetEnemyTurnSprite();
        EnemyManager.Instance.StartTurn();
    }
}
