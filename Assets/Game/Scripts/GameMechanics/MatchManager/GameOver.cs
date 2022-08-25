using UnityEngine;

public class GameOver : MonoBehaviour
{
    public bool IsGameOver { get; private set; } = false;
    
    private GameObject Player;
    private GameObject Objective;
    private LifeSystem PlayerLife;
    private LifeSystem ObjectiveLife;

    private GameEvents gameEvents;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Objective = GameObject.FindGameObjectWithTag("Objective");
        PlayerLife = Player.GetComponent<LifeSystem>();
        ObjectiveLife = Objective.GetComponent<LifeSystem>();
        gameEvents = GetComponent<GameEvents>();
    }
    private void Update()
    {
        if (PlayerLife.currentLife <= 0 || ObjectiveLife.currentLife <= 0)
        {
            gameEvents.WarnGameOver();
            IsGameOver = true;
        }
    }
}
