using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState : int
{
    PRE_ROUND = 0,
    ACTIVE_ROUND,
    POST_ROUND,
    GAME_OVER
}

public class GameController : MonoBehaviour
{
    private UIController ui;
    private EnemyManager enemyManager;
    private Tower tower;

    private Timer preRoundTimer;
    private Timer postRoundTimer;
    private Timer activeRoundTimer;

    GameState state = GameState.PRE_ROUND;

    [SerializeField]
    float roundTimer = 5;

    private int round = 0, spawnsPerRound = 1, spawnsThisRound = 0;

    void Start()
    {
        enemyManager = GameObject.FindObjectOfType<EnemyManager>();
        tower = GameObject.FindObjectOfType<Tower>();
        ui = GameObject.FindObjectOfType<UIController>();
        
        preRoundTimer = new Timer(4f);
        postRoundTimer = new Timer(1.5f);
        activeRoundTimer = new Timer(roundTimer);
    }

    void Update()
    {
        switch (state)
        {
            case GameState.PRE_ROUND:
                DoPreRound();
                break;

            case GameState.ACTIVE_ROUND:
                DoActiveRound();
                break;

            case GameState.POST_ROUND:
                DoPostRound();
                break;

            case GameState.GAME_OVER:
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
                break;
        }
    }

    void NextState()
    { 
        if ((int)state >= 2)
            state = 0;
        else
            state++;
    }

    private void DoPreRound()
    {
        //UI Timer active
        preRoundTimer.Tick();
        ui.ToggleTimer();
        ui.DoTimer(preRoundTimer.GetHand());

        if (preRoundTimer.isDone())
        {
            preRoundTimer.Reset();
            ToActive();
            NextState();
        }
    }

    //Called once before active round
    //Sets up variables for active round
    private void ToActive() 
    {
        round++;
        ui.ToggleTimer(false);
        spawnsThisRound = 0;
        spawnsPerRound += 2;
        activeRoundTimer.Reset();
    }

    private void DoActiveRound()
    {
        if(spawnsThisRound < spawnsPerRound)
        {
            activeRoundTimer.Tick();
            
            if (activeRoundTimer.isDone())
            {
                spawnsThisRound++;
                activeRoundTimer.Reset();
                enemyManager.SpawnWave();
            }
        }
        else if(enemyManager.GetNumEnemies() == 0) //Wait for enemies to die or reach goal before starting next wave
        {
            NextState();
        }
    }

    private void DoPostRound()
    {
        postRoundTimer.Tick();
        ui.NextRoundText();

        if (postRoundTimer.isDone())
        {
            postRoundTimer.Reset();
            NextState();
        }
    }

    public GameState GetGameState()
    {
        return state;
    }

    public void GameOver()
    {
        state = GameState.GAME_OVER;
    }

    public void plusScore(int score)
    {
        ui.DoScore(score);
    }

    void updateRound()
    {
        ui.DoRound(round);
    }

    public void DoHealth(int health)
    {
        ui.DoHealth(health);
    }
}
