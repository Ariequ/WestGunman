using UnityEngine;
using System.Collections;

public class GameUIPanel : BaseUIPanel
{
    private Animator animator;

    enum State
    {
        Normal,
        BeginFire
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
}
