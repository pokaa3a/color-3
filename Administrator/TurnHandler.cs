using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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
            endTurnButton.SetEnemyTurnSprite();
            EnemyManager.Instance.StartTurn();
            // GameStats.Instance.ComputeScore();
        }
        else                // enemy's turn -> player's turn
        {
            playersTurn = true;
            endTurnButton.SetUnpressedSprite();
        }
    }
}
