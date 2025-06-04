using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyInitalizer : MonoBehaviour
{
    public TMP_Text[] stage;
    public TMP_Text[] score;
    public Button[] Enter;

    void Start()
    {
        InitLobby();
    }
    public void InitLobby()
    {
        int nowStage = GameManager.instance.stage;
        for (int i = 0; i < 3; i++)
        {
            UIManager.instance.nowStage[i] = stage[i];
            UIManager.instance.nowStage[i].text = $"{GameManager.instance.chapter} - {i + 1}";
            UIManager.instance.stageScore[i] = score[i];
            UIManager.instance.stageScore[i].text =
            $"{GameManager.instance.goalTable.GetData(GameManager.instance.chapter, i + 1)}";
            UIManager.instance.EnterButton[i] = Enter[i];
            if (i != nowStage - 1)
            {
                UIManager.instance.EnterButton[i].interactable = false;
            }

        }
    }
    public void LoadNextScene()
    {
        GameManager.instance.StartBattle();
    }

}
