using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public string spritePath { get; protected set; }
    public string descriptionPath { get; protected set; }
    public Action action { get; protected set; }

    public Skill() { }
}
