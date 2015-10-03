using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    private IUIPanel currentPanel;
    private Dictionary<string, BaseUIPanel> panelDic;

    public static UIManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    public void Init()
    {
        panelDic = new Dictionary<string, BaseUIPanel>();
        
        BaseUIPanel[] panels = Resources.FindObjectsOfTypeAll<BaseUIPanel>();

        for (int i = 0; i < panels.Length; i++)
        {
            panelDic.Add(panels[i].PanelName, panels[i]);
            panels[i].gameObject.SetActive(false);
        }

        GameController.Instrance.BeginFire += OnBeginFire;
        GameController.Instrance.Win += OnWin;
        GameController.Instrance.Lose += OnLose;
    }

    public void Dispose()
    {
        panelDic.Clear();
        panelDic = null;

        GameController.Instrance.BeginFire -= OnBeginFire;
    }

    void OnWin (object sender, GameControllerEventArgs e)
    {
        GameUIPanel panel = currentPanel as GameUIPanel;
        panel.ShowWin();
    }

    void OnLose (object sender, GameControllerEventArgs e)
    {
        GameUIPanel panel = currentPanel as GameUIPanel;
        panel.ShowLose();
    }

    void OnBeginFire (object sender, GameControllerEventArgs e)
    {
        GameUIPanel panel = currentPanel as GameUIPanel;
        panel.ShowFire();
    }

    public void Show(string name)
    {
        if (currentPanel != null)
        {
            Hide(currentPanel.PanelName);
        }

        panelDic[name].gameObject.SetActive(true);
        panelDic[name].OnShow();

        currentPanel = panelDic[name];
    }

    public void Hide(string name)
    {
        panelDic[name].gameObject.SetActive(false);
        panelDic[name].OnHide();
    }   

    public GameUIPanel GetGameUIPanel()
    {
        return currentPanel as GameUIPanel;
    }
}

