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
        Start, PlayerTurn, EnemyTurn, Win, Lose, Wait
    }

    public BattleState state;

    public GameObject playersPrefeb;
    public GameObject enemyPrefeb;

    private Enemy enemy;
    private Players players;

    public Text dialogText;
    public UIManager playersHUD;
    public UIManager enemyHUD;

    public Transform[] battleChoosePos;
    public GameObject chooseAction;
    public GameObject choosePlane;

    int turn = 1;//行动次数
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        state = BattleState.Start;
        dialogText.text = "";

        StartCoroutine(SetupBattle());
    }

    // Update is called once per frame
    void Update()
    {
        if (state == BattleState.PlayerTurn && turn == 0)
        {
            players.currentAction += Time.deltaTime * players.actionSpeed;
            playersHUD.atUpdate(players.currentAction);
            enemy.currentAction += Time.deltaTime * enemy.actionSpeed;
            enemyHUD.atUpdate(enemy.currentAction);
            if (players.currentAction >= players.maxAction)
            {
                state = BattleState.PlayerTurn;
                players.currentAction = 0;
                turn = 1;
                PlayerTurn();
            }
            else if (enemy.currentAction >= enemy.maxAction)
            {
                state = BattleState.EnemyTurn;
                enemy.currentAction = 0;
                turn = 1;
                StartCoroutine((string)EnemyTurn());

            }
        }
        if (state == BattleState.PlayerTurn && turn > 0)
        {
            BattleChoose();
        }
    }
    private IEnumerator SetupBattle()
    {
        enemy = enemyPrefeb.GetComponent<Enemy>();
        players = playersPrefeb.GetComponent<Players>();

        dialogText.text = enemy.characterName + "出现了！";

        playersHUD.InitHUD(players);
        enemyHUD.InitHUD(enemy);

        yield return new WaitForSeconds(1.5f);
        if (players.actionSpeed > enemy.actionSpeed)
        {
            state = BattleState.PlayerTurn;
        }
        else
        {
            state = BattleState.EnemyTurn;
            StartCoroutine((string)EnemyTurn());
        }
    }
    private void PlayerTurn()
    {
        dialogText.text = "现在是" + players.characterName + "的回合!";
        choosePlane.SetActive(true);
    }
    private void BattleChoose()
    {
        int i = 0;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            i = i - 1;
            chooseAction.transform.position = battleChoosePos[i].position;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            i += 1;
        }
        if (i == -1)
        {
            i = 2;
            chooseAction.transform.position = battleChoosePos[i].position;
        }
        if (i == battleChoosePos.Length)
        {
            i = 0;
            chooseAction.transform.position = battleChoosePos[i].position;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            turn -= 1;
            switch (i)
            {
                case 0:
                    StartCoroutine(PlayerAttack());
                    break;
                case 1:
                    break;
                default:
                    break;
            }
        }
    }
    private IEnumerator PlayerAttack()
    {
        dialogText.text = players.characterName + "使用了攻击";
        bool isDefeated = enemy.TakeDamege(players.attack, enemy.defend);
        yield return new WaitForSeconds(1f);
        float dm = players.attack - enemy.defend;
        dialogText.text = players.characterName + "对" + enemy.characterName + "造成了" + dm + "点伤害";
        enemyHUD.hpUpdate(enemy.currentHealth);

        if (isDefeated)
        {

            state = BattleState.Win;
            EndBattle();
        }
        else
        {
            choosePlane.SetActive(false);
            state = BattleState.Wait;
            StartCoroutine(Wait());
        }
    }
    private IEnumerable EnemyTurn()
    {
        dialogText.text = "现在是" + enemy.characterName + "的回合!";

        yield return new WaitForSeconds(1f);
        dialogText.text = enemy.characterName + "使用了攻击";
        bool isDefeated = enemy.TakeDamege(enemy.attack, players.defend);
        yield return new WaitForSeconds(1f);
        float dm = enemy.attack - players.defend;
        dialogText.text = enemy.characterName + "对" + players.characterName + "造成了" + dm + "点伤害";
        playersHUD.hpUpdate(players.currentHealth);
        if (isDefeated)
        {

            state = BattleState.Lose;
            EndBattle();
        }
        else
        {

            state = BattleState.Wait;
            StartCoroutine(Wait());
            turn -= 1;
        }
    }
    private void EndBattle()
    {
        if (state == BattleState.Lose)
        {
            dialogText.text = "现在是" + enemy.characterName + "的胜利";
        }
        if (state == BattleState.Lose)
        {
            dialogText.text = "现在是" + players.characterName + "的胜利";
        }
    }
}

