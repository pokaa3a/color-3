using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttackConnected : Skill
{
    public SkillAttackConnected() : base()
    {
        // sprite
        spritePath = SpritePath.Skill.Icon.attackConnected;
        descriptionPath = SpritePath.Skill.Description.attackConnected;

        // action
        action = new ActionAttackConnected();
    }
}
