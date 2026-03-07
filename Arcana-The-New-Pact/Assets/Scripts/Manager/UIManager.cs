using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text nameText;
    public Text levelText;

    public Slider hpSlider;
    public Slider mpSlider;
    public Slider atSlider;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitHUD(Character character)
    {
        nameText.text = character.name;
        levelText.text="LV"+character.level.ToString();
        hpSlider.maxValue = character.maxHealth;
        hpSlider.value = character.currentHealth;
        mpSlider.maxValue=character.maxMagic;
        mpSlider.value = character.currentMagic;
    }
    public void hpUpdate(float hp)
    {
        hpSlider.value = hp;
    }
    public void mpUpdate(float mp)
    {
        mpSlider.value = mp;
    }
    public void atUpdate(float at) { 
        atSlider.value = at;
    }
}
