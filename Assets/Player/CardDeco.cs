using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeco
{
    private SpecialDeco cardDeco { set; get; }
    public int order;
    private int cardNum;
    private List<int[,,]> decoList;

    public CardDeco(int _cardNum, int _order, SpecialDeco _deco)
    {
        decoList.Add(new int[_cardNum, _order, (int)_deco]);
    }


}
