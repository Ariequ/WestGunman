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

    public virtual void OnShow()
    {
    }

    public virtual void OnHide()
    {
    }
}
