using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CardDeck
{
    public Card[,] Deck;
    public int[,] SpecialNum;
    [SerializeField] public int[,] numOfCard { private set; get; }
    public Dictionary<Emblem, List<Card>> EmblemListDic;
    public Action<Card> OnCardAdded;
    public Action<Card> OnCardRemoved;

    public CardDeck()
    {
        Init();
    }

    private void Init()
    {
        Deck = new Card[4, 13];
        numOfCard = new int[4, 13];



        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                numOfCard[i, j] = 1;
                Deck[i, j] = new Card((Emblem)i, j + 1);
            }
        }
    }

    private void EmblemDicInit()
    {
        EmblemListDic = new Dictionary<Emblem, List<Card>>();
        foreach (Emblem _em in Enum.GetValues(typeof(Emblem)))
        {
            EmblemListDic[_em] = new List<Card>();
        }
    }

    #region DecoSet/Get
    /// <summary>
    /// 카드에 추가 효과를 주는 Deco를 설정해준다.
    /// </summary>
    /// <param name="_cardNum">카드 숫자(0~51)까지의 숫자를 받으며 (+1) 13 단위로 자동으로 하트, 스페이드, 클로버, 다이아몬드 문양으로 변환해서 계산한다.</param>
    /// <param name="_order">설정하고자하는 카드의 순서인 order 값을 받아 order번째의 카드의 효과를 설정한다. </param>
    void SetDeco(int _cardNum, SpecialDeco _deco, int _order)
    {
        Emblem _em = (Emblem)(_cardNum / 13);
        int _mod = _cardNum % 13;

        if (_order > numOfCard[(int)_em, _mod])
        {
            Debug.Log("order index Error");
            return;
        }

        if (SearchDeco(_em, _cardNum, _order, out Card _card))
        {
            if (_card.deco == _deco) return;
            _card.SetDeco(_deco);
            return;
        }
        EmblemListDic[_em].Add(new Card(_em, _mod, _deco, _order));
    }

    public SpecialDeco GetDeco(int _cardNum, int _order) => GetDeco((Emblem)(_cardNum / 13), _cardNum % 13, _order);
    ///<summary>
    /// 카드의 Deco를 반환한다. 해당 order의 카드의 Deco가 없으면 Deco.none으로 반환한다.
    ///</summary>

    public SpecialDeco GetDeco(Emblem _em, int _cardNum, int _order)
    {
        SearchDeco(_em, _cardNum, _order, out Card _card);
        return _card.deco;
    }

    private bool SearchDeco(Emblem _em, int _cardNum, int _order, out Card _card)
    {
        foreach (Card card in EmblemListDic[_em])
        {
            if (card.CardNum != _cardNum) continue;
            if (card.order != _order) continue;
            _card = new Card(_em, _cardNum, card.deco, _order);
            return true;
        }
        _card = new Card(_em, _cardNum, SpecialDeco.none, _order);
        return false;
    }
    #endregion

    #region Add/RemoveCard 
    public void AddCard(int _cardNum) => AddCard((Emblem)(_cardNum / 13), _cardNum % 13);
    /// <summary>
    /// 해당 카드를 덱에 추가해준다.
    /// </summary>
    public void AddCard(Emblem _em, int _cardNum)
    {
        int _order = numOfCard[(int)_em, _cardNum]++;
        SetDeco((int)_em * 13 + _cardNum, SpecialDeco.none, _order);
        OnCardAdded?.Invoke(new Card(_em, _cardNum));
    }

    public void RemoveCard(int _cardNum, int _order) => RemoveCard((Emblem)(_cardNum / 13), _cardNum % 13, _order);
    /// <summary>
    /// 해당 카드가 덱에 있는지 확인하고 있다면 한장 제거한다.
    /// </summary>
    public void RemoveCard(Emblem _em, int _cardNum, int _order)
    {
        if (numOfCard[(int)_em, _cardNum] == 0) return;
        numOfCard[(int)_em, _cardNum]--;

        if (SearchDeco(_em, _cardNum, _order, out Card _card) &&
        EmblemListDic.TryGetValue(_em, out List<Card> list))
        {
            list.Remove(_card);
        }
        OnCardRemoved?.Invoke(new Card(_em, _cardNum));
    }
    #endregion



}

