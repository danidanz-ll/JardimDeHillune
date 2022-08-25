using TMPro;
using UnityEngine;

public class MatchTimer : MonoBehaviour
{
    [SerializeField] public float timer = 15;
    [SerializeField] private TMP_Text clockText;

    public bool IsTimerFinshed{ get; private set; } = false;
    private GameEvents gameEvents;
    private float oldTimer;

    private void Start()
    {
        gameEvents = GetComponent<GameEvents>();
        oldTimer = timer;

        gameEvents.GameOver += StopTimer;
    }
    private void Update()
    {
        if (!IsTimerFinshed)
        {
            timer -= Time.deltaTime;
            if (clockText != null)
            {
                int minutes = (int)Mathf.Abs(timer / 60.0f);
                int seconds = (int)Mathf.Abs((minutes * 60) - timer);
                string minutesString = minutes.ToString();
                string secondsString = seconds.ToString();
                if (minutes < 10)
                {
                    minutesString = "0" + minutesString;
                }
                if (seconds < 10)
                {
                    secondsString = "0" + secondsString;
                }
                clockText.text = minutesString + ":" + secondsString;
            }
            if (timer <= 0)
            {
                WarnTimerFinished();
                IsTimerFinshed = true;
            }
        }
    }
    private void WarnTimerFinished()
    {
        gameEvents.WarnTimeOver();
    }
    private void StopTimer()
    {
        IsTimerFinshed = true;
    }
}
