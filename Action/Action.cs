using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action
{
    public Action() { }

    public abstract bool Act(Vector2 xy);
}
