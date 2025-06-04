using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitState : IState
{
    private GameManager gm;

    public InitState(GameManager _gm)
    {
        gm = _gm;
    }

    public void Enter()
    {

    }

    public void Update()
    {

    }

    public void Exit()
    {

    }

}
