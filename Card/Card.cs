using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public string spritePath { get; protected set; }
    public string bigSpritePath { get; protected set; }
    public Skill skill { get; protected set; }

    public Card() { }
}
