using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    public Skill() { }

    public abstract bool Act(Vector2 xy);
}
