using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttackConnected : Action
{
    private const int attackAmount = 1;

    public ActionAttackConnected() : base() { }

    public override bool Act(Vector2 xy)
    {
        if (Map.Instance.InsideMap(xy))
        {
            Vector2Int rc = Map.Instance.XYtoRC(xy);
            HashSet<Vector2Int> tiles = Map.Instance.GetConnectedTiles(rc);

            foreach (Vector2Int tileRc in tiles)
            {
                // play attack animation
                Map.Instance.GetTile(tileRc).CallStartCoroutine(AttackCoroutine(tileRc));
                Enemy enemy = Map.Instance.GetTile(tileRc).GetObject<Enemy>();
                // attack enemy
                if (enemy != null)
                {
                    enemy.BeAttacked(attackAmount);
                }
            }
            return true;
        }
        return false;
    }

    // TODO: this function has many duplicates in multiple card objects
    // -> Make it a global function
    public IEnumerator AttackCoroutine(Vector2Int rc)
    {
        var attackEffect = Map.Instance.AddObject<Effect>(rc);
        attackEffect.spritePath = SpritePath.Object.Effect.attack;

        yield return new WaitForSeconds(0.5f);
        Map.Instance.DestroyObject<Effect>(rc);
    }
}
