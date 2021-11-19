using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    public static App instance;

    public enum EGameState { 
        ESTART,
        EGAME,
        EEND    
    }

    private EGameState currentState;

    public EGameState gameState {
        get { return currentState; }
        set {
            currentState = value;
            switch (currentState)
            {
                case EGameState.ESTART:
                    break;
                case EGameState.EGAME:
                    StartGame();
                    break;
                case EGameState.EEND:
                    EndGame();
                    break;
            }
        }
    }

    public Action onGameStart;
    public Action onGameEnd;

    private iGameMode gameMode;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        InitGameMode();
    }

    void InitGameMode() {
        gameMode = FindObjectOfType<GameMode>();
        gameMode.InitGameMode();
    }

    public void StartGame() {
        onGameStart?.Invoke();
        gameMode?.StartGameMode();
    }

    public void EndGame() { 
        onGameEnd?.Invoke();
        gameMode?.StopGameMode();
    }

    public void RewardPoints(float points) {
        gameMode.RewardPoints(points);
    }
}
