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
        AfterFire,
        Lose,
        Win
    }

    private static GameController instance;  

    public event EventHandler<GameControllerEventArgs> GameStart;
    public event EventHandler<GameControllerEventArgs> BeginFire;
    public event EventHandler<GameControllerEventArgs> Win;
    public event EventHandler<GameControllerEventArgs> Lose;

    private Dictionary<GameState, Action> clickHandle = new Dictionary<GameState, Action>();

    private GameState currentState = GameState.BeforeFire;

    private bool gameStarted = false;

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
        GunManManager.Instance.Init();

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
        if (gameStarted && Input.GetMouseButtonUp(0))
        {
            Debug.Log(currentState.ToString());

            Action eventHandle;

            clickHandle.TryGetValue(currentState, out eventHandle);

            if (eventHandle != null)
            {
                eventHandle();
            }
        }
    }    

    void Dispose()
    {
        UIManager.Instance.Dispose();
        GunManManager.Instance.Dispose();

        clickHandle.Clear();
        clickHandle = null;

        instance = null;
    }

    public void StartGame()
    {        
        UIManager.Instance.Show(GameUIPanel.GetPanelName());

        OnStartGame(new GameControllerEventArgs());

        StartRound();

        StartCoroutine("SetGameStartInNextFrame");
    }

    IEnumerator SetGameStartInNextFrame()
    {
        yield return 1;

        gameStarted = true;
    }

    public void GunManFire()
    {
        Debug.Log("Player die");

        currentState = GameState.Lose;

        if (Lose != null)
        {
            Lose(this, new GameControllerEventArgs());
        }

        Invoke("ShowEndUILater", 1f);
    }

    private void ShowEndUILater()
    {
        UIManager.Instance.Show(EndUIPanel.GetPanelName());
    }

    public void Restart()
    {
        Application.LoadLevel(0);
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
        float randomTime = UnityEngine.Random.Range(2f, 4f);

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
        Debug.Log("fire too early");

        CancelInvoke();

        currentState = GameState.Lose;

        if (Lose != null)
        {
            Lose(this, new GameControllerEventArgs());
        }

        Invoke("ShowEndUILater", 1f);
    }

    private void FireClick()
    {
        Debug.Log("Win");

        GunManManager.Instance.RemoveCurrentGunMan();

        StartRound();

        if (Win != null)
        {
            Win(this, new GameControllerEventArgs());
        }
    }

    private void AfterFireClick()
    {
        Debug.Log("already dead");
    }
}
