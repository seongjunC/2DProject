using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyState : IState
{
    private GameManager gm;

    public LobbyState(GameManager _gm)
    {
        gm = _gm;
    }

    public void Enter()
    {
        Debug.Log("Lobby");
        UIManager.instance.LobbyInit();
        SceneManager.LoadScene("LobbyScene");
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}
