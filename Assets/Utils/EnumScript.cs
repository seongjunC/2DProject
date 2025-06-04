using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Emblem { Heart, Spade, Clover, Diamond }

public enum SpecialDeco { none, Golden, Rare, Shiny, Red }

public enum GoodsStatus { Use, Gain }
public enum SelectMode { Play, Discard }
public enum ButtonState { Normal, Pressed, Disabled }

public enum CardStatus { DeckList, BattleDeck, Hand, Graveyard }
// public enum GameState { Init, Lobby, Battle, GameOver, Result }

public enum CardCombinationEnum
{
    HighCard = 5,
    OnePair = 10,
    TwoPair = 20,
    Triple = 25,
    Straight = 30,
    Flush = 35,
    FullHouse = 40,
    FourCard = 60,
    StraightFlush = 100,
    FiveCard = 120,
    FlushHouse = 160,
    FlushFive = 260,
}