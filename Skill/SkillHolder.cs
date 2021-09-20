using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class SkillHolder
{
    public class SkillComponent : MonoBehaviour, IPointerDownHandler,
        IPointerUpHandler, IPointerExitHandler
    {
        const float longPressThreshold = 0.5f;
        SkillHolder skillHolder;

        bool isPressed = false;    // press in previous frame
        bool isLongPressed = false;
        float pressDownStartTime;
        // Vector2 initPosition;

        void Start()
        {
            // initPosition = transform.position;
        }

        void Update()
        {
            if (isPressed && Time.time > pressDownStartTime + longPressThreshold)
            {
                if (!isLongPressed)
                {
                    // CardManager.Instance.LongPressCard(this.card);
                    SkillManager.Instance.LongPressSkill(this.skillHolder.skill);
                }
                isLongPressed = true;
            }
            else
            {
                if (isLongPressed)
                {
                    // CardManager.Instance.StopLongPressCard();
                    SkillManager.Instance.StopLongPressSkill();
                }
                isLongPressed = false;
            }
        }

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            skillHolder.selected = !skillHolder.selected;
            if (skillHolder.selected)
            {
                SkillManager.Instance.selectedSkillHolder = skillHolder;
            }
            else
            {
                SkillManager.Instance.selectedSkillHolder = null;
            }

            isPressed = true;
            pressDownStartTime = Time.time;
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {
            isPressed = false;
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            isPressed = false;
        }

        public void RegisterSkillHolder(SkillHolder skillHolder)
        {
            this.skillHolder = skillHolder;
        }
    }

    private Vector2 _uv;
    public Vector2 uv
    {
        get => _uv;
        set
        {
            _uv = value;
            gameObject.transform.localPosition = _uv;
        }
    }

    private bool _selected = false;
    public bool selected
    {
        get => _selected;
        set
        {
            _selected = value;
            if (_selected)
            {
                Image img = gameObject.transform.Find("selected").GetComponent<Image>() as Image;
                img.enabled = true;
            }
            else
            {
                Image img = gameObject.transform.Find("selected").GetComponent<Image>() as Image;
                img.enabled = false;
            }
        }
    }

    private GameObject gameObject;
    private SkillComponent component;

    private Skill _skill = null;
    public Skill skill
    {
        get => _skill;
        set
        {
            _skill = value;
            SetSprite(_skill.spritePath);
        }
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

    public SkillHolder(GameObject gameObject)
    {
        this.gameObject = gameObject;
        component = this.gameObject.AddComponent<SkillComponent>() as SkillComponent;
        component.RegisterSkillHolder(this);
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
}
