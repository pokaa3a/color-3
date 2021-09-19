using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    private List<Enemy> enemies;
    private int numActionsCompleted = 0;

    // Singleton
    private static EnemyManager _instance;
    public static EnemyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EnemyManager();
            }
            return _instance;
        }
    }

    private EnemyManager()
    {
        enemies = new List<Enemy>();
    }

    public void Summon()
    {
        Enemy minion1 = Map.Instance.AddObject<Enemy>(new Vector2Int(0, 7));
        enemies.Add(minion1);

        Enemy minion2 = Map.Instance.AddObject<Enemy>(new Vector2Int(7, 0));
        enemies.Add(minion2);
    }

    public void StartTurn()
    {
        if (enemies.Count == 0)
        {
            TurnHandler.Instance.SwitchTurn();
            return;
        }

        numActionsCompleted = 0;
        foreach (Enemy e in enemies)
        {
            e.Act();
        }
    }

    public void OneActionCompleted()
    {
        numActionsCompleted++;
        if (numActionsCompleted == enemies.Count)
        {
            TurnHandler.Instance.SwitchTurn();
        }
    }

    public void KillEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
