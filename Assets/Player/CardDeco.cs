using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeco
{
    public SpecialDeco DecoShape { private set; get; }
    public int order { get; private set; }
    public int cardNum { get; private set; }

    public CardDeco(int _cardNum, int _order, SpecialDeco _deco)
    {
        order = _order;
        cardNum = _cardNum;
        DecoShape = _deco;
    }

    public void SetDeco(SpecialDeco _deco)
    {
        DecoShape = _deco;
    }
}
