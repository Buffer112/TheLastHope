using System.Collections;
using System.Collections.Generic;
using TheLastHope.Management.Data;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<ASkill> listSkills = new List<ASkill>();
    public void Init()
    {
        foreach (var skill in listSkills)
        {
            skill.Init();
        }
    }

    // Update is called once per frame
    public void SkillUpdate(SceneData sceneData)
    {
        foreach (var skill in listSkills)
        {
            skill.SkillUpdate(sceneData);
        }
    }
}
