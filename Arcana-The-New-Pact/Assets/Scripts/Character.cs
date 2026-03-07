using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame 
    public string characterName;
    //名字
    public float maxHealth;
    //最大生命值
    public float currentHealth;
    //当前生命值
    public float maxMagic;
    //最大法力值
    public float currentMagic;
    //当前法力值
    public float attack;
    //攻击力
    public float defend;
    //防御力
    public int level;

    public float actionSpeed;

    public float maxAction;

    public float currentAction;

    protected virtual void awake()
    {

    }
    protected virtual void Start()
    {
        currentHealth=maxHealth;
        currentMagic=maxMagic;
    }
    public virtual float ChangeHealth(float num)
    {
        currentHealth = Mathf.Clamp(currentHealth + num, 0,maxHealth);
        if (currentHealth < 0)
        {
            //播放死亡动画
        }
            return currentHealth; 
    }
    public virtual float ChangeMagic(float num)
    {
        currentMagic = Mathf.Clamp(currentMagic + num, 0, maxMagic);
        if (currentHealth <= 0)
        {
            //播放死亡动画
        }
        return currentMagic;
    }
    public bool TakeDamege(float atk,float def)
    {
        float dmg = atk - def;
        ChangeHealth(dmg);
        if (currentHealth <= 0)
            return true;
        else
            return false;

        
    }
}
