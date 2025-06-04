using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneInit : MonoBehaviour
{
    public RectTransform HandPanel;
    public Button PlayButton;
    public Button DiscardButton;
    public TMP_Text combiText;
    public GameObject ConfirmPanel;
    public Button ConfirmButton;
    public TMP_Text Confirmtext;
    public TMP_Text ScoreBoard;

    void Start()
    {
        UIManager ui = UIManager.instance;
        if (ui != null)
        {
            Debug.Log("Battle ui initialized");
            PlayButton.onClick.AddListener(ui.OnClickPlayButton);
            DiscardButton.onClick.AddListener(ui.OnClickDiscardButton);
        }
        Init();
    }

    void Init()
    {
        UIManager.instance.handPanel = HandPanel;
        UIManager.instance.PlayButton = PlayButton;
        UIManager.instance.DiscardButton = DiscardButton;
        UIManager.instance.CombinationText = combiText;
        UIManager.instance.confirmPanel = ConfirmPanel;
        UIManager.instance.confirmButton = ConfirmButton;
        UIManager.instance.confirmText = Confirmtext;
        UIManager.instance.scoreBoard = ScoreBoard;
        UIManager.instance.ClearHand();
    }
}
