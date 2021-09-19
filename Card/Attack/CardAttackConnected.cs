using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAttackConnected : Card
{
    private const int attackAmount = 1;

    public CardAttackConnected() : base()
    {
        // Sprite
        spritePath = SpritePath.Card.Small.attackConnected;
        bigSpritePath = SpritePath.Card.Big.attackConnected;
    }

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

            // BFS
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            HashSet<Vector2Int> seen = new HashSet<Vector2Int>();
            queue.Enqueue(rc);
            seen.Add(rc);
            // play attack animation
            Map.Instance.GetTile(rc).CallStartCoroutine(AttackCoroutine(rc));
            // attack enemy
            Enemy enemy = Map.Instance.GetTile(rc).GetObject<Enemy>();
            if (enemy != null)
            {
                enemy.BeAttacked(attackAmount);
            }

            while (queue.Count > 0)
            {
                int qCount = queue.Count;
                for (int i = 0; i < qCount; ++i)
                {
                    Vector2Int curRc = queue.Dequeue();
                    Vector2Int[] directions = {
                        Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left };
                    foreach (Vector2Int d in directions)
                    {
                        Vector2Int nextRc = curRc + d;
                        if (Map.Instance.InsideMap(nextRc) &&
                            Map.Instance.GetTile(nextRc).color == attackColor &&
                            !seen.Contains(nextRc))
                        {
                            queue.Enqueue(nextRc);
                            seen.Add(nextRc);
                            // play attack animation
                            Map.Instance.GetTile(nextRc).CallStartCoroutine(AttackCoroutine(nextRc));

                            // attack enemy
                            Enemy e = Map.Instance.GetTile(nextRc).GetObject<Enemy>();
                            if (e != null)
                            {
                                e.BeAttacked(attackAmount);
                            }
                        }
                    }
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
