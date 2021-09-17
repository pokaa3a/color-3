using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MapObject
{
    // TODO: should this be integrated to MapObject?
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

    public Tower() { }

    public Tower(Vector2Int rc) : base(rc)
    {
        gameObject.name = "Tower";

        spriteWH = new Vector2(
            Map.Instance.tileWH.x * 0.5f,
            Map.Instance.tileWH.y * 0.6f);
        this.spritePath = SpritePath.Object.tower;

        this.rc = rc;

        // Set up life text
        GameObject textObject = new GameObject("lifeText");
        textObject.transform.SetParent(this.gameObject.transform);
        lifeText = textObject.AddComponent<TextMesh>() as TextMesh;
        textObject.transform.localPosition = new Vector2(0f, 0.7f);
        textObject.transform.localScale = new Vector2(0.04f, 0.08f);
        lifeText.fontSize = 100;
        lifeText.text = $"{life}/{maxLife}";
        lifeText.anchor = TextAnchor.MiddleCenter;
        lifeText.color = new Color32(0, 255, 50, 255);   // red
    }

    public void BeAttacked(int attackAmount)
    {
        life = Mathf.Max(life - attackAmount, 0);
        if (life == 0)
        {
            Collapse();
        }
    }

    private void Collapse()
    {
        Map.Instance.GetTile(rc).DestroyObject<Tower>();
        Map.Instance.TowerCollapse(this);
    }
}
