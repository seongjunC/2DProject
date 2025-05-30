using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <param name="Chip">상점에서 물건을 구매할 수 있는 코인</param>
    public int Chip { get; private set; }
    public CardDeck Deck;
    public List<Card> Hand;
    public List<Card> Field;
    public List<Card> Graveyard;

    void Start()
    {
        Deck = new CardDeck();
        Chip = 0;

    }

    void Draw(int _num)
    {

    }
}
