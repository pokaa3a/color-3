using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePath
{
    public class Tile
    {
        public const string empty = "Sprites/tile/tile_empty";
        public const string red = "Sprites/tile/tile_red";
        public const string yellow = "Sprites/tile/tile_yellow";
        public const string blue = "Sprites/tile/tile_blue";
    }

    public class Object
    {
        public class Character
        {
            public const string square = "Sprites/object/character/square";
            public const string circle = "Sprites/object/character/triangle";
            public const string triangle = "Sprites/object/character/triangle";
        }
        public class Enemy
        {
            public const string minion = "Sprites/object/enemy/minion";
        }
        public class Effect
        {
            public const string attack = "Sprites/object/effect/attack";
            public const string attackAttempt = "Sprites/object/effect/attack_attempt";
            public const string move = "Sprites/object/effect/move";
        }
        public const string tower = "Sprites/object/tower";
    }

    public class UI
    {
        public class Button
        {
            public const string endTurnButtonEnemy = "Sprites/ui/button/endTurnButton_enemy";
            public const string endTurnButtonPressed = "Sprites/ui/button/endTurnButton_pressed";
            public const string endTurnButtonUnpressed = "Sprites/ui/button/endTurnButton_unpressed";
        }
    }
}
