using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TMP_Text gameOverText;
    public bool IsGameOver { get; private set; } = false;
    
    private GameObject Player;
    private GameObject Objective;
    private LifeSystem PlayerLife;
    private LifeSystem ObjectiveLife;

    private GameEvents gameEvents;

    private LifeSystem lifeSystem;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Objective = GameObject.FindGameObjectWithTag("Objective");
        PlayerLife = Player.GetComponent<LifeSystem>();
        ObjectiveLife = Objective.GetComponent<LifeSystem>();
        gameEvents = GetComponent<GameEvents>();

        PlayerLife.DeathEvent += PlayerDeath;
        ObjectiveLife.DeathEvent += PlayerDeath;
    }
    private void Update()
    {
        if (PlayerLife.currentLife <= 0 || ObjectiveLife.currentLife <= 0)
        {
            gameEvents.WarnGameOver();
            IsGameOver = true;
        }
    }
    private void PlayerDeath()
    {
        Debug.Log("O jogador morreu!");
        gameEvents.WarnGameOver();
        ShowGameOver();
        IsGameOver = true;
    }
    private void ShowGameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.text = "Você perdeu!";
        }
    }
}
