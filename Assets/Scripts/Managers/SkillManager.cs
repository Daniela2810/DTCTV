using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }
    private Dictionary<DetectiveAbility, int> skillLevels;
    private int detectivePoints = 100;

    public int SkillPoints => detectivePoints;

    public void SkillPointIncrease(int amount)
    {
        detectivePoints += amount;
    }

    public void SkillPointDecrease(int amount)
    {
        detectivePoints -= amount;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        InitializeSkills();
    }

    private void InitializeSkills()
    {
        skillLevels = new Dictionary<DetectiveAbility, int>
        {
            { DetectiveAbility.Charisma, 0 },
            { DetectiveAbility.Investigation, 0 },
            { DetectiveAbility.Tools, 0 }
        };
    }

    public void AdjustSkillPoints(int amount)
    {
        detectivePoints += amount;
        Debug.Log("Adjusted Skill Points by " + amount);
    }

    public void LevelUp(DetectiveAbility skill)
    {
        if (skillLevels.ContainsKey(skill) && detectivePoints >= CalculateSkillCost(skill))
        {
            skillLevels[skill]++;
            AdjustSkillPoints(-CalculateSkillCost(skill));
            Debug.Log("Leveled up " + skill);
        }
        else
        {
            Debug.Log("Not enough points or skill not found");
        }
    }

    public int GetSkillLevel(DetectiveAbility skill)
    {
        return skillLevels.TryGetValue(skill, out int level) ? level : 0;
    }

    public int CalculateSkillCost(DetectiveAbility skill)
    {
        int level = GetSkillLevel(skill);
        return level + 1; // Each level costs the current level + 1 to advance.
    }
}
