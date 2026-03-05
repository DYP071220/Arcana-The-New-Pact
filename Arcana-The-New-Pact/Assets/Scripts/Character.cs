using System.Collections;
using System.Collections.Generic;
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

    protected virtual void awake()
    {

    }
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
