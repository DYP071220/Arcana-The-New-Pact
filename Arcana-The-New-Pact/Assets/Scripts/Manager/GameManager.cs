using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameState GameState;


    /*Awake()在脚本实例被加载时调用
     * 比 Start() 执行更早
     * 即使脚本组件未启用也会执行
     * 用来写单例  */
    private void Awake()
    {
        //这句也是抄的
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 销毁重复的实例
            return;
        }

        Instance = this;
    }
    void Start()
    {
        ChangeState(GameState.MapGeneration);
    }

    void Update()
    {

    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (GameState)
        {
            case GameState.GameMainInterface://游戏主界面（未开始）
                break;
            case GameState.MapGeneration://地图生成
                break;
            case GameState.EnemySpawning://敌人生成
                break;
            case GameState.UnitPlacement://单位放置
                break;
            case GameState.UnitCommanding://单位指挥
                break;
            case GameState.UnitActions://单位行动
                break;
            case GameState.VictorySettlement://胜利结算
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(GameState),newState,null);//这句不会，我抄的
        }
    }
}
public enum GameState
{
    GameMainInterface,//游戏主界面（未开始）
    MapGeneration,//地图生成
    EnemySpawning,//敌人生成
    UnitPlacement,//单位放置
    UnitCommanding,//单位指挥
    UnitActions,//单位行动
    VictorySettlement,//胜利结算
}