using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CardTest : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteArray;
    [SerializeField] private GameObject prefab;
    [SerializeField][Range(0, 51)] private int index;
    private SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        spriteArray = Resources.LoadAll<Sprite>("CardSprite/Playing_Cards");
        render = prefab.GetComponent<SpriteRenderer>();
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        render.sprite = spriteArray[index];
    }
}
