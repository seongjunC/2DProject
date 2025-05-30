using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <param name="Chip">상점에서 물건을 구매할 수 있는 코인</param>
    public int Chip { get; private set; }
    public BattleStat EquipmentStat;
    public CardDeck Deck;
    public Dictionary<CardStatus, List<Card>> CardListDic;


    void Start()
    {
        Deck = new CardDeck();
        CardDicInit();
        DeckSetting();
        Deck.OnCardAdded += OnCardAddedDeck;
        Deck.OnCardRemoved += OnCardRemoveDeck;
        Chip = 0;
    }

    private void CardDicInit()
    {
        CardListDic = new Dictionary<CardStatus, List<Card>>();
        foreach (CardStatus _status in Enum.GetValues(typeof(CardStatus)))
        {
            CardListDic[_status] = new List<Card>();
        }
    }

    public void DeckSetting()
    {
        foreach (Card card in Deck.Deck)
        {
            CardListDic[CardStatus.DeckList].Add(card);
        }
    }

    /// <summary>
    /// 덱에서 카드가 추가되었을 때 이벤트로 받아 현재 덱에 추가한다.
    /// </summary>
    private void OnCardAddedDeck(Card _card)
    {
        CardListDic[CardStatus.DeckList].Add(_card);
        CardListDic[CardStatus.DeckList].Sort((a, b) =>
        {
            int result = a.Emblem.CompareTo(b.Emblem);
            if (result != 0) return result;
            return a.CardNum.CompareTo(b.CardNum);
        });
    }

    /// <summary>
    /// 덱에서 카드가 삭제되었을 때 이벤트로 받아서 현재 덱에서 삭제한다.
    /// </summary>
    private void OnCardRemoveDeck(Card _card)
    {
        CardListDic[CardStatus.DeckList].Remove(_card);
    }

    /// <summary>
    /// 배틀을 시작할 때의 초기 설정
    /// </summary>
    /// <param name="startNum"> 시작할 때 패의 매수</param>
    public void BattleSetting(BattleStat _stat)
    {
        List<Card> BattleDeck = CardListDic[CardStatus.BattleDeck];
        CardListDic[CardStatus.BattleDeck] = CardListDic[CardStatus.DeckList].Select(card => card.Clone()).ToList();
        Shuffle(BattleDeck);
        Battle(_stat, BattleDeck);
    }

    public void Battle(BattleStat _stat, List<Card> BattleDeck)
    {
        Draw(_stat, BattleDeck);
    }

    /// <summary>
    /// 덱에서 핸드로 카드를 뽑아오고 배틀 덱에서 제거한다.
    /// </summary>
    public void Draw(BattleStat _stat, List<Card> BattleDeck)
    {
        List<Card> Hand = CardListDic[CardStatus.Hand];
        for (int i = 0; i < _stat.DrawNum; i++)
        {
            Hand.Add(BattleDeck[0].Clone());
            BattleDeck.RemoveAt(0);
        }
    }

    /// <summary>
    /// Fisher-Yates Shuffle 알고리즘을 이용한 셔플
    /// </summary>
    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;

        for (int i = n - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    public void ChipAction(int _chip, GoodsStatus _status)
    {
        if (_status == GoodsStatus.Gain) Chip += _chip;
        if (_status == GoodsStatus.Use) Chip -= _chip;
        else return;
    }

}
