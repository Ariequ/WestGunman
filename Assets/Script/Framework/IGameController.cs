using UnityEngine;
using System.Collections;
using System;

public interface IGameController
{
    event EventHandler<GameControllerEventArgs> GameStart;

    void StartGame();
}

public class GameControllerEventArgs : EventArgs
{

}