using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;
    public GridManager gridManager;
    public CardManager cardManager;
    public GameObject QuitPanel;

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
        ChangeState(GameState.MapGeneration);
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

    public void OpenSettings()
    {
        QuitPanel.SetActive(true);
    }
    public void CancelSettings()
    {
        QuitPanel.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("退出游戏");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else 
            Application.Quit();
#endif
        Debug.Log("");
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