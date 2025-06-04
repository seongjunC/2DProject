using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Transform handPanel;
    public GameObject cardSlotPrefab;
    public Button StartButton;
    public Button QuitButton;

    public Button PlayButton;
    public Button DiscardButton;
    public TMP_Text[] nowStage;
    public TMP_Text[] stageScore;
    public Button[] EnterButton;


    public GameObject confirmPanel;
    public Button confirmButton;
    public TMP_Text confirmText;


    public TMP_Text CombinationText;

    public TMP_Text scoreBoard;

    [SerializeField] private List<Card> selectedCards = new List<Card>();

    void Awake()
    {
        SingletonInit();
    }


    public void ShowHand(List<Card> hand)
    {

        selectedCards.Clear();
        PlayButton.interactable = false;
        DiscardButton.interactable = false;

        ClearPanel(handPanel);

        foreach (Card card in hand)
        {
            var slot = Instantiate(cardSlotPrefab, handPanel);
            slot.GetComponent<CardSlot>().SetCard(card);
        }
    }

    public void OnCardSelected(Card card, CardSlot slot)
    {
        if (!selectedCards.Contains(card))
        {
            if (selectedCards.Count >= 5) return;

            selectedCards.Add(card);
            slot.SetSelected(true);
        }
        else
        {
            selectedCards.Remove(card);
            slot.SetSelected(false);
        }
        CardCombinationEnum _comName = GameManager.instance.player.stat.getCombination(selectedCards, GameManager.instance.player);
        CombinationText.text = _comName.ToString();

        bool active = selectedCards.Count >= 1;
        PlayButton.interactable = active;
        DiscardButton.interactable = active;
    }

    public void OnClickStartButton()
    {
        GameManager.instance.SetLobby();
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }

    public void OnClickPlayButton()
    {
        if (selectedCards.Count < 1) return;
        GameManager.instance.player.stat.SetPlayerInputDone(selectedCards, SelectMode.Play);
        PlayButton.interactable = false;
        DiscardButton.interactable = false;
    }

    public void OnClickDiscardButton()
    {
        if (selectedCards.Count < 1) return;
        GameManager.instance.player.stat.SetPlayerInputDone(selectedCards, SelectMode.Discard);
        PlayButton.interactable = false;
        DiscardButton.interactable = false;
    }

    public void OnClickEnterButton()
    {
        GameManager.instance.StartBattle();
    }

    public void ClearPanel(Transform panel)
    {
        foreach (Transform child in panel)
            Destroy(child.gameObject);
    }

    public void ClearHand() => ClearPanel(handPanel);

    public void LobbyInit()
    {
        nowStage = new TMP_Text[3];
        stageScore = new TMP_Text[3];
        EnterButton = new Button[3];
    }

    public void ShowconfirmPanel(string _situ)
    {
        confirmPanel.SetActive(true);
        confirmButton.onClick.RemoveAllListeners();
        if (_situ == "StageClear")
        {
            confirmText.text = $"{GameManager.instance.chapter} - {GameManager.instance.stage} Clear!";
            confirmButton.onClick.AddListener(() =>
            {
                confirmPanel.SetActive(false);
                GameManager.instance.SetLobby();
            });
        }
        else if (_situ == "GameOver")
        {
            confirmText.text = $"GameOver...";
            confirmButton.onClick.AddListener(() =>
            {
                confirmPanel.SetActive(false);
                GameManager.instance.Start();
            });
        }
    }
}
