using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIPanel : BaseUIPanel
{
    public Text ScoreLabel;
    public Text GunManTimeLabel;
    public Text PlayerTimeLabel;

    public GameObject FireSample;

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
        ScoreLabel.text = gameData.Score.ToString();
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
        ScoreLabel.text = score.ToString();
    }

    public void UpdateGunManShootTimeLabel(float time)
    {
        GunManTimeLabel.text = time.ToString("0.00");
    }

    public void UpdatePlayerShootTime(float milliseconds)
    {
        PlayerTimeLabel.text = (milliseconds/1000).ToString("0.00");
    }
}
