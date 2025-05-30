using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{

    public Emblem Emblem { get; private set; }
    public int CardNum { get; private set; }
    public SpecialDeco deco;

    public Card(Emblem _emblem, int _num)
    {
        Emblem = _emblem;
        CardNum = _num;
        deco = SpecialDeco.none;
    }
}

