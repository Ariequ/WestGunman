using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameController : MonoBehaviour, IGameController
{
    enum GameState
    {
        BeforeFire,
        Fire,
        AfterFire
    }

    private static GameController instance;  

    public event EventHandler<GameControllerEventArgs> GameStart;
    public event EventHandler<GameControllerEventArgs> BeginFire;

    private Dictionary<GameState, Action> clickHandle = new Dictionary<GameState, Action>();

    private GameState currentState = GameState.BeforeFire;

    public static GameController Instrance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameController>();
            }

            return instance;
        }
    }

    void Awake()
    {
        UIManager.Instance.Init();

        clickHandle.Add(GameState.BeforeFire, BeforFireClick);
        clickHandle.Add(GameState.Fire, FireClick);
        clickHandle.Add(GameState.AfterFire, AfterFireClick);
    }

    void Start()
    {
        UIManager.Instance.Show(StarUIPanel.GetPanelName());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickHandle[currentState]();
        }
    }       



    public void StartGame()
    {        
        UIManager.Instance.Show(GameUIPanel.GetPanelName());

        OnStartGame(new GameControllerEventArgs());

        StartRound();
    }

    protected virtual void OnStartGame(GameControllerEventArgs e)
    {
        if(GameStart != null)
        {
            GameStart(this, e);
        }
    }

    private void StartRound()
    {
        GunManManager.Instance.AddRandomGunMan();

        BeginFireInRandomTime();
    }

    private void BeginFireInRandomTime()
    {
        float randomTime = UnityEngine.Random.Range(0, 0.5f);

        Invoke("Fire", randomTime);
    }

    private void Fire()
    {
        if (BeginFire != null)
        {
            BeginFire(this, new GameControllerEventArgs());
        }

        currentState = GameState.Fire;
    }

    private void BeforFireClick()
    {
    }

    private void FireClick()
    {
    }

    private void AfterFireClick()
    {
        
    }
}
