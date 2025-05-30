using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card", order = int.MaxValue)]
public class CardObject : ScriptableObject
{
    private Card card;
    private SpriteRenderer spriteRenderer;
}
