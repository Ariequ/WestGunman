using UnityEngine;
using System.Collections;

public interface IUIPanel
{
    string PanelName
    {
        get;
    }
    void OnShow(GameData gameData);
    void OnHide();
}
