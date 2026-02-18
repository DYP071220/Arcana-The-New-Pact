using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;
    public GameObject StartGameButton;
    public GameObject QuitGameButton;
    public GridManager gridManager;
    public CardManager cardManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
    }
    void Start()
    {
        ChangeState(GameState.GameMainInterface);
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (GameState)
        {
            case GameState.GameMainInterface://游戏主界面（未开始）
                break;
            case GameState.MapGeneration://地图生成
                gridManager.GenerateGrid();
                cardManager.AddCardTo(5);
                break;
            case GameState.PullCards://抽卡
                break;
            case GameState.UnitActions://单位行动
                break;
            case GameState.VictorySettlement://胜利结算
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(GameState),newState,null);//这句不会，我抄的
        }
    }

    public void StartGame()
    {
        StartGameButton.SetActive(false);
        QuitGameButton.SetActive(false);
        ChangeState(GameState.MapGeneration);

    }
}
public enum GameState
{
    GameMainInterface,//游戏主界面（未开始）
    MapGeneration,//地图生成
    PullCards,//抽卡
    UnitActions,//单位行动
    VictorySettlement,//胜利结算
}