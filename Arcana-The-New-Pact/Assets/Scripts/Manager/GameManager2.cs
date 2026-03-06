using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager2 instance;
    public enum BattleState
    {
        Start, PlayerTurn, EnemyTurn, Win, Lose
    }
    
    public BattleState state;

    public GameObject playersPrefeb;
    public GameObject enemyPrefeb;

    private Enemy enemy;
    private Players players;

    public Text dialogText;
    public UIManager playersHUD;
    public UIManager enemyHUD;
   
 
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        dialogText.text = "";
        state = BattleState.Start;
        StartCoroutine(SetupBattle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator SetupBattle()
    {
        enemy=enemyPrefeb.GetComponent<Enemy>();
        players = playersPrefeb.GetComponent<Players>();

        dialogText.text =enemy.characterName+"≥ˆœ÷¡À£°";

        playersHUD.InitHUD(players);
        enemyHUD.InitHUD(enemy);
        
        yield return new WaitForSeconds(1.5f);
    }
}
