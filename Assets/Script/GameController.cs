using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using Debug = UnityEngine.Debug;

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

    private GameData gameData;

    private Stopwatch stopWatch;

    private GameUIPanel gameUIPanel;

    public GameData GameData
    {
        get
        {
            return gameData;
        }
    }

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

        gameData = new GameData();
        gameUIPanel = UIManager.Instance.GetGameUIPanel();

        stopWatch = new Stopwatch();
        stopWatch.Start();
    }

    void Start()
    {
        UIManager.Instance.Show(GameUIPanel.GetPanelName());
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

        gameData = null;

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

        CancelInvoke();
        StopAllCoroutines();

        currentState = GameState.Lose;

        if (Lose != null)
        {
            Lose(this, new GameControllerEventArgs());
        }

        Invoke("ShowEndUILater", 1f);
    }

    private void ShowEndUILater()
    {
        gameUIPanel.ShowEndUI();
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
        GunMan added = GunManManager.Instance.AddRandomGunMan();

        gameData.CurrentShootTime = added.ShootTime;

        gameUIPanel.UpdateGunManShootTimeLabel(gameData.CurrentShootTime);
        gameUIPanel.UpdatePlayerShootTime(0);

        BeginFireInRandomTime();

        currentState = GameState.BeforeFire;
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

        gameData.RoundFireBeginTime = stopWatch.ElapsedMilliseconds;

        InvokeRepeating("UpdatePlayerShootTime", 0, 0.1f);
    }

    private void UpdatePlayerShootTime()
    {
        gameUIPanel.UpdatePlayerShootTime(stopWatch.ElapsedMilliseconds - gameData.RoundFireBeginTime);
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

        CancelInvoke();

        gameData.PlayerShootTime = stopWatch.ElapsedMilliseconds;

        Debug.Log(gameData.PlayerShootTime);
        Debug.Log(gameData.RoundFireBeginTime);

        gameUIPanel.UpdatePlayerShootTime(gameData.PlayerShootTime - gameData.RoundFireBeginTime);

        if (Win != null)
        {
            Win(this, new GameControllerEventArgs());
        }                   

        StartCoroutine("AddBonus");

        GunManManager.Instance.RemoveCurrentGunMan();
    }

    IEnumerator AddBonus()
    {
        yield return new WaitForSeconds(2f);

        float start = (gameData.PlayerShootTime - gameData.RoundFireBeginTime) / 1000f;

        float target = gameData.CurrentShootTime;

        while (start + 0.01f < target)
        {
            Debug.Log(start.ToString());

            gameData.Score += 10;
            start += 0.01f;

            gameUIPanel.UpdatePlayerShootTime(start * 1000);
            gameUIPanel.UpdateScoreLabel(gameData.Score);
            gameUIPanel.UpdateHighestScoreLabel(gameData.HighestScore);

            yield return 1;
        }

        gameUIPanel.UpdatePlayerShootTime(target * 1000);
        Invoke("StartRound", 0.5f);
    }

    private void AfterFireClick()
    {
        Debug.Log("already dead");
    }
}
