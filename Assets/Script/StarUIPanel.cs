using UnityEngine;
using System.Collections;

public class StarUIPanel : BaseUIPanel
{
    public static string GetPanelName()
    {
        return "StarUIPanel";
    }

    public override string PanelName
    {
        get
        {
            return StarUIPanel.GetPanelName();
        }
    }

    public void OnStartBtnClick()
    {
        Debug.Log("on start ui panel");

        GameController.Instrance.StartGame();
    }
}
