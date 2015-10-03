using UnityEngine;
using System.Collections;

public class GameUIPanel : BaseUIPanel
{
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
}
