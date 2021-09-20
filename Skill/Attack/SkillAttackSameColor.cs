using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttackSameColor : Skill
{
    public SkillAttackSameColor() : base()
    {
        // sprite
        spritePath = SpritePath.Skill.Icon.attackSameColor;
        descriptionPath = SpritePath.Skill.Description.attackSameColor;

        // action
        action = new ActionAttackSameColor();
    }
}
