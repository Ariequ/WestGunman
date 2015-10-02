using UnityEngine;
using System.Collections;

public interface IUIPanel
{
    string PanelName
    {
        get;
    }
    void OnShow();
    void OnHide();
}
