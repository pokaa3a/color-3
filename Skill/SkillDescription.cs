using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDescription
{
    private GameObject gameObject;
    private string _spritePath;
    public string spritePath
    {
        get => _spritePath;
        set
        {
            _spritePath = value;
            SetSprite(_spritePath);
        }
    }

    private bool _enabled;
    public bool enabled
    {
        get => _enabled;
        set
        {
            if (value)
            {
                Enable();
            }
            else
            {
                Disable();
            }
            _enabled = value;
        }
    }

    public SkillDescription(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

    public void Enable()
    {
        Image img = gameObject.GetComponent<Image>() as Image;
        img.enabled = true;
    }

    public void Disable()
    {
        Image img = gameObject.GetComponent<Image>() as Image;
        img.enabled = false;
    }

    private void SetSprite(string spritePath)
    {
        Image img = gameObject.GetComponent<Image>() as Image;
        if (img == null)
        {
            img = gameObject.AddComponent<Image>() as Image;
        }
        img.sprite = Resources.Load<Sprite>(spritePath);
    }
}
