using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public string spritePath { get; protected set; }
    public string bigSpritePath { get; protected set; }

    public Card()
    {

    }

    // Called when touching and this card has been selected
    public abstract bool Act(Vector2 xy);
}
