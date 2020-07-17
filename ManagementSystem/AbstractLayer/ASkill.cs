using System.Collections;
using System.Collections.Generic;
using TheLastHope.Helpers;
using TheLastHope.Management.Data;
using UnityEngine;


public abstract class ASkill : MonoBehaviour
{
    //Время восстановления способности
    [SerializeField] public float cooldown = 1;
    //Список объектов, с которыми взаимодействует скилл (Может быть пустым)
    protected Timer _delay = new Timer();
    protected string nameKey;
    // Start is called before the first frame update
    public abstract void Ability(SceneData sceneData);
    public abstract void Init();
    public virtual void Activate(SceneData sceneData)
    {
        if (_delay.Elapsed == -1)
        {
            print("SkillActivated.");
            Ability(sceneData);
            _delay.Start(cooldown);
        }
    }

    // Update is called once per frame
    public virtual void SkillUpdate(SceneData sceneData)
    {
        if (Input.GetButtonDown(nameKey))
        {    
            Activate(sceneData);
        }
        _delay.TimerUpdate();
    }
}
