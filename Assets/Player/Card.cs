using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public Emblem Emblem { get; private set; }
    public int CardNum { get; private set; }
    public SpecialDeco deco;

    public Card(Emblem _emblem, int _num) : this(_emblem, _num, SpecialDeco.none)
    {
    }
    public Card(Emblem _emblem, int _num, SpecialDeco _deco)
    {
        Emblem = _emblem;
        CardNum = _num;
        deco = _deco;
    }

    public Card Clone()
    {
        return new Card(this.Emblem, this.CardNum, this.deco);
    }
}

