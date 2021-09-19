using System.Collections;
using System.Collections.Generic;
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

    public Character() { }

    public Character(Vector2Int rc) : base(rc)
    {
        gameObject.name = "character";

        spriteWH = new Vector2(
            Map.Instance.tileWH.x * 0.6f,
            Map.Instance.tileWH.y * 0.6f);

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
