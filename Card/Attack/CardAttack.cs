using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAttack : Card
{
    private const int attackAmount = 1;

    public CardAttack() : base()
    {
        // Sprite
        spritePath = SpritePath.Card.Small.attack;
        bigSpritePath = SpritePath.Card.Big.attack;
    }

    public override bool Act(Vector2 xy)
    {
        if (Map.Instance.InsideMap(xy))
        {
            Vector2Int rc = Map.Instance.XYtoRC(xy);
            Enemy enemy = Map.Instance.GetTile(rc).GetObject<Enemy>();

            if (enemy != null)
            {
                enemy.BeAttacked(attackAmount);
                Map.Instance.GetTile(rc).CallStartCoroutine(AttackCoroutine(rc));
                return true;
            }
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
