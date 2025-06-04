using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpriteManager : Singleton<CardSpriteManager>
{
    public Sprite[] cardSprites;

    void Awake()
    {
        cardSprites = Resources.LoadAll<Sprite>("CardSprite/Playing_Cards");
    }


    public Sprite GetCardSprites(Emblem _em, int _num)
    {
        int index = ((int)_em * 13) + (_num - 1);
        if (index < 0 || index >= cardSprites.Length) return null;
        return cardSprites[index];
    }
}
