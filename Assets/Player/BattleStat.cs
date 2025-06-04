using System;
using System.Collections;
using System.Collections.Generic;
using CustomUtility.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BattleStat
{
    // 드로우 갯수
    private int DrawNum;
    // 드로우 갯수 초기값 (추후 조커를 구현하면 해당값에 따라 인게임에서 조정)
    public int drawNum;
    public bool IsPlayerDone { get; private set; } = false;

    // 버리기 횟수
    private int Discard;
    // 버리기 횟수 초기값 (추후 조커를 구현하면 해당값에 따라 인게임에서 조정)
    public int discard;
    // 플레이 횟수
    private int Hands;
    // 플레이 횟수 초기값 (추후 조커를 구현하면 해당값에 따라 인게임에서 조정)
    public int hands;
    public int score;
    public int stageScore;
    public Dictionary<CardCombinationEnum, int> CombMag;
    public Action OnHandsOver;
    public Action OnStageClear;

    private List<Card> selectedCards = new();
    public SelectMode mode { private set; get; }



    public void Init()
    {
        drawNum = 8;
        discard = 3;
        hands = 3;
        DictionaryInit();
    }

    public void ResetPlayerInput(CsvTable _GoalTable, Vector2Int stagePos)
    {
        IsPlayerDone = false;
        selectedCards.Clear();
        stageScore = int.Parse(_GoalTable.GetData(stagePos));
        score = 0;

        DrawNum = drawNum;
        Discard = discard;
        Hands = hands;
    }

    public List<Card> GetSelectedCards() => selectedCards;

    public void EvaluateCombination(List<Card> cards, Player _player)
    {
        CardCombinationEnum comb = CardCombination.CalCombination(cards, _player, out int sumCardNum);

        score += (int)comb * CombMag[comb] * sumCardNum;
        UIManager.instance.scoreBoard.text = score.ToString();
        Hands--;
        if (score >= stageScore)
        {
            OnStageClear?.Invoke();
        }
        else if (Hands <= 0)
        {
            OnHandsOver?.Invoke();
        }
    }

    public CardCombinationEnum getCombination(List<Card> card, Player player)
    {
        return CardCombination.CalCombination(card, player, out int _);
    }

    public void DictionaryInit()
    {
        CombMag = new Dictionary<CardCombinationEnum, int>
        {
            { CardCombinationEnum.HighCard, 1 },
            { CardCombinationEnum.OnePair, 2},
            { CardCombinationEnum.TwoPair, 2},
            { CardCombinationEnum.Triple, 3},
            { CardCombinationEnum.Straight, 4},
            { CardCombinationEnum.Flush, 4},
            { CardCombinationEnum.FullHouse, 4},
            { CardCombinationEnum.FourCard, 7},
            { CardCombinationEnum.StraightFlush, 8},
            { CardCombinationEnum.FiveCard, 12},
            { CardCombinationEnum.FlushHouse, 14},
            { CardCombinationEnum.FlushFive, 16}
        };
    }

    public void MagnificationAdjust(CardCombinationEnum _comb, int _upAmount)
    {
        CombMag[_comb] += _upAmount;
    }

    public void SetPlayerInputDone(List<Card> cards, SelectMode _mode)
    {
        IsPlayerDone = true;
        selectedCards = cards;
        mode = _mode;
    }

    public void SetDoneFalse()
    {
        IsPlayerDone = false;
    }

    public void UseDiscard()
    {
        Discard--;
    }
}
