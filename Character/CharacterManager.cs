using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager
{
    private List<Character> characters = new List<Character>();

    private Character _selectedCharacter = null;
    public Character selectedCharacter
    {
        get => _selectedCharacter;
        set
        {
            _selectedCharacter = value;
            foreach (Character character in characters)
            {
                if (character == _selectedCharacter)
                {
                    character.selected = true;
                }
                else
                {
                    character.selected = false;
                }
            }

            if (_selectedCharacter != null)
            {
                SkillManager.Instance.Enable();
            }
            else
            {
                SkillManager.Instance.Disable();
            }
        }
    }

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
        characters.Add(square);
        characters.Add(circle);
        characters.Add(triangle);

        // TODO: Too messy
        // sprites
        square.originalSprite = SpritePath.Object.Character.square;
        square.selectedSprite = SpritePath.Object.Character.squareSelected;
        square.spritePath = square.originalSprite;
        circle.originalSprite = SpritePath.Object.Character.circle;
        circle.selectedSprite = SpritePath.Object.Character.circleSelected;
        circle.spritePath = circle.originalSprite;
        triangle.originalSprite = SpritePath.Object.Character.triangle;
        triangle.selectedSprite = SpritePath.Object.Character.triangleSelected;
        triangle.spritePath = triangle.originalSprite;

        // skills
        square.skills = new List<Skill>{
            (Skill)(new SkillAttack()),
            (Skill)(new SkillAttackConnected()),
            (Skill)(new SkillAttackSameColor())};
        circle.skills = new List<Skill>{
            new SkillAttack(), new SkillAttackConnected(), new SkillAttackSameColor()};
        triangle.skills = new List<Skill>{
            new SkillAttack(), new SkillAttackConnected(), new SkillAttackSameColor()};
    }
}
