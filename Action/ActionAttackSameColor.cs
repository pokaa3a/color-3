using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttackSameColor : Action
{
    private const int attackAmount = 1;

    public ActionAttackSameColor() : base() { }

    public override bool Act(Vector2 xy)
    {
        if (Map.Instance.InsideMap(xy))
        {
            Vector2Int rc = Map.Instance.XYtoRC(xy);
            Color attackColor = Map.Instance.GetTile(rc).color;
            if (attackColor == Color.Empty)
            {
                return false;
            }

            for (int r = 0; r < Map.rows; ++r)
            {
                for (int c = 0; c < Map.rows; ++c)
                {
                    Vector2Int curRc = new Vector2Int(r, c);
                    if (Map.Instance.GetTile(curRc).color == attackColor)
                    {
                        // play attack animation
                        Map.Instance.GetTile(curRc).CallStartCoroutine(
                            AttackCoroutine(new Vector2Int(r, c)));

                        // attack enemy
                        Enemy enemy = Map.Instance.GetTile(curRc).GetObject<Enemy>();
                        if (enemy != null)
                        {
                            enemy.BeAttacked(attackAmount);
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }

    public IEnumerator AttackCoroutine(Vector2Int rc)
    {
        var attackEffect = Map.Instance.AddObject<Effect>(rc);
        attackEffect.spritePath = SpritePath.Object.Effect.attack;

        yield return new WaitForSeconds(0.5f);
        Map.Instance.DestroyObject<Effect>(rc);
    }
}
