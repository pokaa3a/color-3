using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionAttack : Action
{
    private const int attackAmount = 1;
    private HashSet<Vector2Int> actionAvailableRCs = new HashSet<Vector2Int>();

    public ActionAttack() : base() { }

    public override void Initialize()
    {
        if (SceneManager.GetActiveScene().name == SceneHandler.sceneCharacter)
        {
            Vector2Int charRc = CharacterManager.Instance.selectedCharacter.rc;

            // N
            if (Map.Instance.InsideMap(charRc + Vector2Int.up))
            {
                Effect n = Map.Instance.AddObject<Effect>(charRc + Vector2Int.up);
                n.spritePath = SpritePath.Object.Effect.move;
                actionAvailableRCs.Add(charRc + Vector2Int.up);
            }
            // S
            if (Map.Instance.InsideMap(charRc + Vector2Int.down))
            {
                Effect s = Map.Instance.AddObject<Effect>(charRc + Vector2Int.down);
                s.spritePath = SpritePath.Object.Effect.move;
                actionAvailableRCs.Add(charRc + Vector2Int.down);
            }
            // E
            if (Map.Instance.InsideMap(charRc + Vector2Int.right))
            {
                Effect e = Map.Instance.AddObject<Effect>(charRc + Vector2Int.right);
                e.spritePath = SpritePath.Object.Effect.move;
                actionAvailableRCs.Add(charRc + Vector2Int.right);
            }
            // W
            if (Map.Instance.InsideMap(charRc + Vector2Int.left))
            {
                Effect w = Map.Instance.AddObject<Effect>(charRc + Vector2Int.left);
                w.spritePath = SpritePath.Object.Effect.move;
                actionAvailableRCs.Add(charRc + Vector2Int.left);
            }
        }
    }

    public override void Deinitialize()
    {
        if (SceneManager.GetActiveScene().name == SceneHandler.sceneCharacter)
        {
            foreach (Vector2Int tileRc in actionAvailableRCs)
            {
                Map.Instance.GetTile(tileRc).DestroyObject<Effect>();
            }
            actionAvailableRCs.Clear();
        }
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
