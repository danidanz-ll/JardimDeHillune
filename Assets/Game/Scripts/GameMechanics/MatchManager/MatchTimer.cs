using UnityEngine;

public class MatchTimer : MonoBehaviour
{
    [SerializeField] private float timer = 15;

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
            if (timer < 0)
            {
                Debug.Log("O tempo acabou!");
                IsTimerFinshed = true;
            }
        }
    }
}
