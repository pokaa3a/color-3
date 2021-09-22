using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public Action() { }

    // Called (by ActionManager) when this action is selected
    public virtual void Initialize() { }

    // Called (by ActionManager) when this action is de-selected
    public virtual void Deinitialize() { }

    public virtual bool Act(Vector2 xy) { return true; }

    public virtual int Consume(Vector2Int rc)
    {
        HashSet<Vector2Int> connectedRCs = Map.Instance.GetConnectedTiles(rc);
        foreach (Vector2Int tileRc in connectedRCs)
        {
            Map.Instance.GetTile(tileRc).SetColor(Color.Empty);
        }
        Map.Instance.RefillTiles();
        return connectedRCs.Count;
    }
}
