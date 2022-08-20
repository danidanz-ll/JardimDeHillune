using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public event Action MatchWin;
    public event Action GameOver;
    public event Action TimeOver;

    public void WarnMatchWin()
    {
        MatchWin.Invoke();
    }
    public void WarnGameOver()
    {
        GameOver.Invoke();
    }
    public void WarnTimeOver()
    {
        TimeOver.Invoke();
    }
}
