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
}
