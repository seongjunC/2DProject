using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Emblem Emblem { get; private set; }
    public int CardNum { get; private set; }

    public Card(Emblem _emblem, int _num)
    {
        Emblem = _emblem;
        CardNum = _num;
    }
}

