using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIPanel : BaseUIPanel
{
    public Text scoreLabel;
    public Text highestLabel;
    public Text gunManTimeLabel;
    public Text playerTimeLabel;

    public GameObject startBtn;
    public GameObject replayBtn;

    private Animator animator;

    enum State
    {
        Normal,
        BeginFire,
        Win,
        Lose
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnShow(GameData gameData)
    {
        scoreLabel.text = gameData.Score.ToString();
        highestLabel.text = gameData.HighestScore.ToString();
    }

    public static string GetPanelName()
    {
        return "GameUIPanel";
    }

    public override string PanelName
    {
        get
        {
            return GameUIPanel.GetPanelName();
        }
    }

    public void ShowFire()
    {
        animator.SetInteger("state", (int)State.BeginFire);
    }

    public void OnFireFinish()
    {
        animator.SetInteger("state", (int)State.Normal);
    }

    public void ShowWin()
    {
        animator.SetInteger("state", (int)State.Win);
    }

    public void OnWinFinish()
    {
        animator.SetInteger("state", (int)State.Normal);
    }

    public void ShowLose()
    {
        animator.SetInteger("state", (int)State.Lose);
    }

    public void OnLoseFinish()
    {
        animator.SetInteger("state", (int)State.Normal);
    }

    public void UpdateScoreLabel(int score)
    {
        scoreLabel.text = score.ToString();
    }

    public void UpdateHighestScoreLabel(int highest)
    {
        highestLabel.text = highest.ToString();
    }

    public void UpdateGunManShootTimeLabel(float time)
    {
        gunManTimeLabel.text = time.ToString("0.00");
    }

    public void UpdatePlayerShootTime(float milliseconds)
    {
        playerTimeLabel.text = (milliseconds/1000).ToString("0.00");
    }

    public void OnStartBtnClick()
    {
        Debug.Log("on start ui panel");

        startBtn.SetActive(false);
        GameController.Instrance.StartGame();
    }

    public void OnRestartBtnClick()
    {        
        GameController.Instrance.Restart();
    }

    public void ShowEndUI()
    {
        replayBtn.SetActive(true);
    }
}
