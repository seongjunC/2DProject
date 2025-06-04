using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InitSceneLoader : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _quitButton;

    void Start()
    {
        UIManager.instance.StartButton = _startButton;
        UIManager.instance.QuitButton = _quitButton;
    }

    public void LoadNextScene()
    {
        GameManager.instance.SetLobby();
    }
}
