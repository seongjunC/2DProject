using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : IState
{
    private GameManager gm;

    public GameOverState(GameManager gm)
    {
        this.gm = gm;
    }

    public void Enter()
    {
        Debug.Log("Game Over!");
    }

    public void Update() { }

    public void Exit() { }
}
