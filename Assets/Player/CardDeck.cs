using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public Card[,] Deck;
    public CardDeco Deco;
    [SerializeField] public int[,] numOfCard { private set; get; }
    [SerializeField] private List<Func<int[,,]>> SpecialList;




    // Start is called before the first frame update
    void Start()
    {
        Deck = new Card[4, 13];
        numOfCard = new int[4, 13];
        SpecialList = new List<Func<int[,,]>>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                numOfCard[i, j] = 1;
                Deck[i, j] = new Card((Emblem)i, j);
            }
        }
    }

    void SetDeco(Emblem _emblem, SpecialDeco _deco, int _order)
    {
        SpecialList.Add(() => new int[(int)_emblem, (int)_deco, _order]);
    }

    void GetDeco()
    {

    }


}

