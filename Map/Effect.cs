using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MapObject
{
    public Effect() { }

    public Effect(Vector2Int rc) : base(rc)
    {
        gameObject.name = "effect";
    }
}
