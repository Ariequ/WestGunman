using UnityEngine;
using System.Collections;

public class EndUIPanel : BaseUIPanel
{
    public static string GetPanelName()
    {
        return "EndUIPanel";
    }

    public override string PanelName
    {
        get
        {
            return EndUIPanel.GetPanelName();
        }
    }

    public void OnRestartBtnClick()
    {
        GameController.Instrance.Restart();
    }
}
