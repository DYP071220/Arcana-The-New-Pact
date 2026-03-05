using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager2 instance;
    public GameObject playerprefeb;
    public GameObject enemyprefeb;

    private Enemy enemy;
    private Player player;

    public Text dialogText;
    public UIManager playerHUD;
    public UIManager enemyHUD;
    public enum BattleState
    {
        Start,PlayerTurn,EnemyTurn,Win,Lose
    }
  public BattleState state;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        state = BattleState.Start;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator SetupBattle()
    {
        enemy=enemyprefeb.GetComponent<Enemy>();
        player = playerprefeb.GetComponent<Player>();
        yield return new WaitForSeconds(1.5f);
    }
}
