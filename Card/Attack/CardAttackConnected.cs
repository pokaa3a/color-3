using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAttackConnected : Card
{
    private const int attackAmount = 1;

    public CardAttackConnected() : base()
    {
        // Sprite
        spritePath = SpritePath.Card.Small.attackConnected;
        bigSpritePath = SpritePath.Card.Big.attackConnected;

        // action
        this.action = new ActionAttackConnected();
    }
}
