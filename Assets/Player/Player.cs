using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <param name="Chip">상점에서 물건을 구매할 수 있는 코인</param>
    public int Chip { get; private set; }
    public CardDeck Deck;
    public List<Card> DeckList;
    public List<Card> BattleDeck;
    public List<Card> Hand;
    public List<Card> Field;
    public List<Card> Graveyard;


    void Start()
    {
        Deck = new CardDeck();
        DeckSetting();
        Deck.OnCardAdded += OnCardAddedDeck;
        Chip = 0;
    }

    public void DeckSetting()
    {
        DeckList = new List<Card>();
        foreach (Card card in Deck.Deck)
        {
            DeckList.Add(card);
        }
    }

    public void OnCardAddedDeck(Card _card)
    {
        DeckList.Add(_card);
        DeckList.Sort((a, b) =>
        {
            int result = a.Emblem.CompareTo(b.Emblem);
            if (result != 0) return result;
            return a.CardNum.CompareTo(b.CardNum);
        });
    }

    public void OnCardRemoveDeck(Card _card)
    {
        DeckList.Remove(_card);
    }

    void BattleSetting(int startNum)
    {
        BattleDeck = DeckList.Select(card => card.Clone()).ToList();
        Shuffle(BattleDeck);
        Draw(startNum);
    }

    public void Draw(int startNum)
    {
        for (int i = 0; i < startNum; i++)
        {
            Hand = BattleDeck.Select(card => card.Clone()).ToList();
            BattleDeck.RemoveAt(0);
        }
    }

    /// <summary>
    /// Fisher-Yates Shuffle 알고리즘을 이용한 셔플플
    /// </summary>
    public void Shuffle<T>(List<T> list)
    {
        int n = list.Count;

        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
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
