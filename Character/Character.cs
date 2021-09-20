using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Character : MapObject
{
    private TextMesh lifeText;
    private const int maxLife = 5;
    private int _life = maxLife;
    public int life
    {
        get => _life;
        set
        {
            _life = value;
            lifeText.text = $"{_life}/{maxLife}";
        }
    }

    public class CharacterComponent : MonoBehaviour
    {
        Character character;
        public void RegisterCharacter(Character character)
        {
            this.character = character;
        }

        void OnMouseDown()
        {
            character.selected = !character.selected;
            if (character.selected)
            {
                CharacterManager.Instance.selectedCharacter = character;
            }
            else
            {
                CharacterManager.Instance.selectedCharacter = null;
            }
        }
    }

    public string originalSprite;
    public string selectedSprite;

    private bool _selected = false;
    public bool selected
    {
        get => _selected;
        set
        {
            _selected = value;
            if (_selected)
            {
                this.spritePath = selectedSprite;
            }
            else
            {
                this.spritePath = originalSprite;
            }
        }
    }

    public List<Skill> skills = new List<Skill>();

    public Character() { }

    public Character(Vector2Int rc) : base(rc)
    {
        gameObject.name = "character";

        spriteWH = new Vector2(
            Map.Instance.tileWH.x * 0.6f,
            Map.Instance.tileWH.y * 0.6f);

        // add component
        CharacterComponent characterComponent =
            this.gameObject.AddComponent<CharacterComponent>() as CharacterComponent;
        characterComponent.RegisterCharacter(this);
        this.gameObject.AddComponent<BoxCollider>();

        // Set up life text
        GameObject textObject = new GameObject("lifeText");
        textObject.transform.SetParent(this.gameObject.transform);
        lifeText = textObject.AddComponent<TextMesh>() as TextMesh;
        textObject.transform.localPosition = new Vector2(0f, 0.7f);
        textObject.transform.localScale = new Vector2(0.04f, 0.08f);
        lifeText.fontSize = 100;
        lifeText.text = $"{life}/{maxLife}";
        lifeText.anchor = TextAnchor.MiddleCenter;
        lifeText.color = new Color32(0, 255, 0, 255);
    }
}
