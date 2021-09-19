using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAttackSameColor : Card
{
    private const int attackAmount = 1;

    public CardAttackSameColor() : base()
    {
        // Sprite
        spritePath = SpritePath.Card.Small.attackSameColor;
        bigSpritePath = SpritePath.Card.Big.attackSameColor;

        // action
        this.action = new ActionAttackSameColor();
    }
}
