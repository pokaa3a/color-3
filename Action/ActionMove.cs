using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMove : Action
{
    public MapObject mapObject;

    public ActionMove(MapObject mapObject) : base()
    {
        this.mapObject = mapObject;
    }

    public override bool Act(Vector2 xy)
    {
        if (Map.Instance.InsideMap(xy))
        {
            Vector2Int rc = Map.Instance.XYtoRC(xy);
            if (rc == mapObject.rc) return false;

            // TODO: Make it generic
            if (mapObject is Tower)
            {
                Map.Instance.GetTile(mapObject.rc).RemoveObject<Tower>();
            }
            else if (mapObject is Character)
            {
                Map.Instance.GetTile(mapObject.rc).RemoveObject<Character>();
                ((Character)mapObject).hasMoved = true;
            }
            else if (mapObject is Enemy)
            {
                Map.Instance.GetTile(mapObject.rc).RemoveObject<Enemy>();
            }
            Map.Instance.GetTile(rc).InsertObject(mapObject);
        }
        return false;
    }
}
