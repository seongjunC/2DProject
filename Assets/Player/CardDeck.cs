using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public Card[,] Deck;
    public CardDeco Deco;
    public int[,] SpecialNum;
    [SerializeField] public int[,] numOfCard { private set; get; }
    public Dictionary<Emblem, List<CardDeco>> EmblemLists;

    void Start()
    {
        Init();
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

        if (SearchDeco(_em, _cardNum, _order, out CardDeco OutDeco))
        {
            if (OutDeco.DecoShape == _deco) return;
            OutDeco.SetDeco(_deco);
            return;
        }
        EmblemLists[_em].Add(new CardDeco(_mod, _order, _deco));

    }

    public SpecialDeco GetDeco(int _cardNum, int _order) => GetDeco((Emblem)(_cardNum / 13), _cardNum % 13, _order);
    ///<summary>
    /// 카드의 Deco를 반환한다. 해당 order의 카드의 Deco가 없으면 Deco.none으로 반환한다.
    ///</summary>
    public SpecialDeco GetDeco(Emblem _em, int _cardNum, int _order)
    {
        if (SearchDeco(_em, _cardNum, _order, out CardDeco OutDeco)) return OutDeco.DecoShape;
        return SpecialDeco.none;
    }

    private bool SearchDeco(Emblem _em, int _cardNum, int _order, out CardDeco _deco)
    {
        foreach (CardDeco decos in EmblemLists[_em])
        {
            if (decos.cardNum != _cardNum) continue;
            if (decos.order != _order) continue;
            _deco = decos;
            return true;
        }
        _deco = null;
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
        numOfCard[(int)_em, _cardNum]++;
    }

    public void RemoveCard(int _cardNum) => RemoveCard((Emblem)(_cardNum / 13), _cardNum % 13);
    /// <summary>
    /// 해당 카드가 덱에 있는지 확인하고 있다면 한장 제거한다.
    /// </summary>
    public void RemoveCard(Emblem _em, int _cardNum)
    {
        if (numOfCard[(int)_em, _cardNum] == 0) return;
        numOfCard[(int)_em, _cardNum]--;
    }
    #endregion

    private void Init()
    {
        Deck = new Card[4, 13];
        numOfCard = new int[4, 13];

        EmblemLists = new Dictionary<Emblem, List<CardDeco>>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                numOfCard[i, j] = 1;
                Deck[i, j] = new Card((Emblem)i, j);
            }
        }
    }


}

