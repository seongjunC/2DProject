using System;
using System.Collections;
using System.Collections.Generic;
using CustomUtility.IO;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    [SerializeField] private CsvTable GoalTable;
    public CsvTable goalTable => GoalTable;
    public Player player;
    public StateMachine stateMachine;

    public int chapter;
    public int stage;
    public bool IsGameOver;
    public bool IsStageClear;


    public void Awake()
    {
        SingletonInit();
        stateMachine = new StateMachine();
        IsGameOver = false;
    }

    public void Start()
    {
        player = new Player();
        player.Init();
        chapter = 1;
        stage = 1;
        CsvReader.Read(GoalTable);
        stateMachine.ChangeState(new InitState(this));
    }

    public void Update()
    {
        stateMachine.Update();
    }

    public void SetInit()
    {
        stateMachine.ChangeState(new InitState(this));
    }

    public void SetGameOver()
    {
        IsGameOver = true;
        Debug.Log("SEtgaov");
    }

    public void SetStageClear()
    {
        IsStageClear = true;
    }

    public void StartBattle()
    {
        stateMachine.ChangeState(new BattleState(this));
    }

    public void SetLobby()
    {
        stateMachine.ChangeState(new LobbyState(this));
    }
}

