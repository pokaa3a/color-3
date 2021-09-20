using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SkillManager
{
    private const int numSkills = 3;
    private List<SkillHolder> skillHolders = new List<SkillHolder>();
    private List<Skill> skills = new List<Skill>();
    private SkillDescription skillDescription;

    public Skill selectedSkill = null;
    private SkillHolder _selectedSkillHolder = null;
    public SkillHolder selectedSkillHolder
    {
        get => _selectedSkillHolder;
        set
        {
            _selectedSkillHolder = value;
            foreach (SkillHolder skillHolder in skillHolders)
            {
                if (skillHolder != _selectedSkillHolder)
                {
                    skillHolder.selected = false;
                }
            }

            ActionManager.Instance.selectedAction = null;
            if (_selectedSkillHolder != null)
            {
                ActionManager.Instance.selectedAction = _selectedSkillHolder.skill.action;
            }
        }
    }

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

    private SkillManager()
    {
        for (int iSkill = 0; iSkill < numSkills; ++iSkill)
        {
            GameObject obj = GameObject.Find(ObjectPath.skillHolder + iSkill.ToString());
            SkillHolder skillHolder = new SkillHolder(obj);
            skillHolders.Add(skillHolder);
        }

        GameObject skillDescriptionObj = GameObject.Find(ObjectPath.skillDescription);
        skillDescription = new SkillDescription(skillDescriptionObj);
        skillDescription.enabled = false;
    }

    public void LongPressSkill(Skill skill)
    {
        if (skill != null)
        {
            skillDescription.spritePath = skill.descriptionPath;
            skillDescription.enabled = true;
        }
    }

    public void StopLongPressSkill()
    {
        skillDescription.enabled = false;
    }

    public void SetSkills(List<Skill> skills)
    {
        Assert.IsTrue(skills.Count == numSkills);
        this.skills = skills;

        for (int iSkill = 0; iSkill < skills.Count; ++iSkill)
        {
            skillHolders[iSkill].skill = skills[iSkill];
        }
    }

    public void Enable()
    {
        if (CharacterManager.Instance.selectedCharacter != null)
        {
            SetSkills(CharacterManager.Instance.selectedCharacter.skills);
        }

        foreach (SkillHolder skillHolder in skillHolders)
        {
            skillHolder.Enable();
        }
    }

    public void Disable()
    {
        foreach (SkillHolder skillHolder in skillHolders)
        {
            skillHolder.Disable();
        }
    }
}
