using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    public Skill selectedSkill = null;

    // Singleton
    private static SkillManager _instance;
    public static SkillManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SkillManager();
            }
            return _instance;
        }
    }

    private SkillManager() { }

    public void Act(Vector2 xy)
    {
        if (selectedSkill == null) return;

        if (selectedSkill.Act(xy))
        {
            // actions of this skill have completed
            selectedSkill = null;
        }
    }
}
