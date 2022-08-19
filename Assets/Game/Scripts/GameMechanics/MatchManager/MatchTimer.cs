using TMPro;
using UnityEngine;

public class MatchTimer : MonoBehaviour
{
    [SerializeField] public float timer = 15;
    [SerializeField] private TMP_Text clockText;

    public bool IsTimerFinshed{ get; private set; } = false;
    private float oldTimer;

    private void Start()
    {
        oldTimer = timer;
    }
    private void Update()
    {
        if (!IsTimerFinshed)
        {
            timer -= Time.deltaTime;
            if (clockText != null)
            {
                int minutes = (int)(timer / 60.0f);
                int seconds = (int)Mathf.Abs((minutes * 60) - timer);
                clockText.text = minutes.ToString() + ":" + seconds.ToString();
            }
            if (timer < 0)
            {
                IsTimerFinshed = true;
            }
        }
    }
}
