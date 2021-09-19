using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager
{
    // Singleton
    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CharacterManager();
            }
            return _instance;
        }
    }

    private CharacterManager() { }

    public void Summon()
    {
        Character square = Map.Instance.AddObject<Character>(new Vector2Int(0, 0));
        Character circle = Map.Instance.AddObject<Character>(new Vector2Int(0, 1));
        Character triangle = Map.Instance.AddObject<Character>(new Vector2Int(0, 2));

        square.spritePath = SpritePath.Object.Character.square;
        circle.spritePath = SpritePath.Object.Character.circle;
        triangle.spritePath = SpritePath.Object.Character.triangle;
    }
}
