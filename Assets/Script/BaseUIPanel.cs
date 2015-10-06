using UnityEngine;
using System.Collections;

public class BaseUIPanel : MonoBehaviour,IUIPanel
{
    public virtual string PanelName
    {
        get
        {
            return "";
        }
    }

    public virtual void OnShow(GameData gameData)
    {
    }

    public virtual void OnHide()
    {
    }
}
