using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : Skill
{
    public SkillAttack() : base()
    {
        // sprite
        spritePath = SpritePath.Skill.Icon.attack;
        descriptionPath = SpritePath.Skill.Description.attack;

        // action
        action = new ActionAttack();
    }
}
