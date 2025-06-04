using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CustomUtility.IO;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class BattleState : IState
{
    GameManager gm;

    public BattleState(GameManager _gm)
    {
        gm = _gm;
    }

    public void Enter()
    {
        SceneManager.LoadScene("BattleScene");
        gm.IsStageClear = false;

        gm.player.stat.OnHandsOver += gm.SetGameOver;
        gm.player.stat.OnStageClear += gm.SetStageClear;

        gm.player.BattleInit();
        gm.player.stat.ResetPlayerInput(gm.goalTable, new Vector2Int(gm.chapter, gm.stage));
        gm.StartCoroutine(BattleLoop());
    }

    public void Update()
    {
    }

    public void Exit()
    {
        gm.player.stat.OnHandsOver -= gm.SetGameOver;
        gm.player.stat.OnStageClear -= gm.SetStageClear;
        if (gm.stage == 3)
        {
            gm.chapter += 1;
            gm.stage = 1;
        }
        else gm.stage += 1;
    }


    public IEnumerator BattleLoop()
    {
        while (!gm.IsGameOver && !gm.IsStageClear)
        {
            yield return gm.StartCoroutine(PlayerTurn());
        }
        if (gm.IsStageClear) UIManager.instance.ShowconfirmPanel("StageClear");
        if (gm.IsGameOver) UIManager.instance.ShowconfirmPanel("GameOver");
    }

    public IEnumerator PlayerTurn()
    {
        gm.player.Draw(gm.player.stat.drawNum - gm.player.Hand.Count);
        yield return new WaitUntil(() => UIManager.instance != null && UIManager.instance.handPanel != null);
        UIManager.instance.ShowHand(gm.player.GetHand());
        yield return new WaitUntil(() => gm.player.stat.IsPlayerDone);

        List<Card> selectedCards = gm.player.stat.GetSelectedCards();
        if (gm.player.stat.mode == SelectMode.Discard)
        {
            gm.player.Discard(selectedCards);
        }
        else if (gm.player.stat.mode == SelectMode.Play)
        {
            gm.player.stat.EvaluateCombination(selectedCards, gm.player);
            gm.player.Discard(selectedCards);
        }
        gm.player.stat.SetDoneFalse();
    }

}
